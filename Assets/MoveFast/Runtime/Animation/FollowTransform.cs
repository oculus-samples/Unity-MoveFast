/*
 * Copyright (c) Meta Platforms, Inc. and affiliates.
 * All rights reserved.
 *
 * Licensed under the Oculus SDK License Agreement (the "License");
 * you may not use the Oculus SDK except in compliance with the License,
 * which is provided at the time of installation or download, or which
 * otherwise accompanies this software in either electronic or hard copy form.
 *
 * You may obtain a copy of the License at
 *
 * https://developer.oculus.com/licenses/oculussdk/
 *
 * Unless required by applicable law or agreed to in writing, the Oculus SDK
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Moves this transform to the position of another transform every frame,
    /// with optional modifications of the other transforms pose.
    /// Used to make the Prompts follow thier target objects and face the user
    /// </summary>
    public class FollowTransform : MonoBehaviour
    {
        public Transform Source;
        public bool _assignMainCamera = false;
        public FollowPosition positionSettings;
        public FollowRotation rotationSettings;
        public When when = When.Update;

        void Awake()
        {
            positionSettings.Noise.offset = UnityEngine.Random.value * 10;
            rotationSettings.Noise.offset = UnityEngine.Random.value * 10;
        }

        private void OnEnable()
        {
            UpdatePose(smoothing: false);
        }

        private void Update()
        {
            if ((when & When.Update) != 0)
            {
                UpdatePose();
            }
        }

        private void LateUpdate()
        {
            if ((when & When.LateUpdate) != 0)
            {
                UpdatePose();
            }
        }

        private void FixedUpdate()
        {
            if ((when & When.FixedUpdate) != 0)
            {
                UpdatePose();
            }
        }

        void UpdatePose(bool smoothing = true)
        {
            if (_assignMainCamera && Application.isPlaying && !Source)
            {
                Camera main = Camera.main;
                if (main) { Source = main.transform; }
            }

            if (Source)
            {
                positionSettings.UpdatePosition(transform, Source, smoothing);
                rotationSettings.UpdateRotation(transform, Source, smoothing);
            }
        }

        [Flags]
        public enum When
        {
            Update = 1 << 0,
            LateUpdate = 1 << 1,
            FixedUpdate = 1 << 2
        }

        [Serializable]
        public struct FollowPosition
        {
            public bool enabled;
            public Vector3 PositionOffset;
            public OffsetSpace PositionOffsetSpace;
            public Vector3Mask PositionMask;
            [Range(0, 0.99f)]
            public float PositionSmoothing;
            public Noise3 Noise;

            public void UpdatePosition(Transform forTransform, Transform fromSource, bool smoothing)
            {
                if (!enabled) { return; }

                Vector3 sourcePosition = fromSource.position + GetPositionOffset(forTransform, fromSource) + Noise.Evaluate(Time.time);

                Vector3 position = forTransform.position;
                if ((PositionMask & Vector3Mask.X) != 0) { position.x = sourcePosition.x; }
                if ((PositionMask & Vector3Mask.Y) != 0) { position.y = sourcePosition.y; }
                if ((PositionMask & Vector3Mask.Z) != 0) { position.z = sourcePosition.z; }

                bool immediate = PositionSmoothing == 0 || !smoothing;
                forTransform.position = immediate ? position : Vector3.Lerp(forTransform.position, position, 1 - Mathf.Pow(PositionSmoothing, Time.deltaTime));
            }

            private Vector3 GetPositionOffset(Transform transform, Transform Source)
            {
                switch (PositionOffsetSpace)
                {
                    case OffsetSpace.World: return PositionOffset;
                    case OffsetSpace.Target: return Source.TransformVector(PositionOffset);
                    case OffsetSpace.This: return transform.TransformVector(PositionOffset);
                    default: throw new System.Exception($"Cant handle offset space {PositionOffsetSpace}");
                }
            }
        }

        [Serializable]
        public struct FollowRotation
        {
            public bool enabled;
            public Rotation RotationType;
            public OffsetSpace UpDirection;
            public bool PrioritizeUp;
            public Vector3 RotationOffset;
            [Range(0, 0.99f)]
            public float RotationSmoothing;
            public Noise3 Noise;

            public void UpdateRotation(Transform forTransform, Transform fromSource, bool smoothing)
            {
                if (!enabled) { return; }

                Vector3 up = GetUpDirection(forTransform, fromSource);
                Vector3 forward = GetForwardDirection(forTransform, fromSource);

                if (forward == Vector3.zero || up == Vector3.zero) return;

                Quaternion rotation = Quaternion.LookRotation(forward, up) * Quaternion.Euler(RotationOffset);
                rotation = Quaternion.Euler(Noise.Evaluate(Time.time)) * rotation;

                bool immediate = RotationSmoothing == 0 || !smoothing;
                forTransform.rotation = immediate ? rotation : Quaternion.Lerp(forTransform.rotation, rotation, 1 - Mathf.Pow(RotationSmoothing, Time.deltaTime));
            }

            private Vector3 GetForwardDirection(Transform forTransform, Transform fromSource)
            {
                Vector3 forward = RotationType == Rotation.LookAt ? fromSource.position - forTransform.position : fromSource.forward;
                if (PrioritizeUp) { forward = (forward - Vector3.Project(forward, GetUpDirection(forTransform, fromSource))).normalized; }
                return forward;
            }

            private Vector3 GetUpDirection(Transform forTransform, Transform fromSource)
            {
                switch (UpDirection)
                {
                    case OffsetSpace.World: return Vector3.up;
                    case OffsetSpace.Target: return fromSource.up;
                    case OffsetSpace.This: return forTransform.parent ? forTransform.parent.up : Vector3.up;
                    default: throw new System.Exception($"Cant handle offset space {UpDirection}");
                }
            }
        }

        public enum Rotation { Rotation, LookAt }
        public enum OffsetSpace { World, Target, This }

        [Serializable, Flags]
        public enum Vector3Mask
        {
            X = 1 << 0,
            Y = 1 << 1,
            Z = 1 << 2
        }

        [Serializable]
        public struct Noise3
        {
            public Vector3 amplitude;
            public float frequency;
            [HideInInspector]
            public float offset;

            public Vector3 Evaluate(float time)
            {
                Vector3 result = Vector3.zero;
                if (amplitude.x != 0) { result.x += amplitude.x * (Mathf.PerlinNoise((time * frequency), offset + 1) - 0.5f); }
                if (amplitude.y != 0) { result.y += amplitude.y * (Mathf.PerlinNoise((time * frequency), offset + 2) - 0.5f); }
                if (amplitude.z != 0) { result.z += amplitude.z * (Mathf.PerlinNoise((time * frequency), offset + 3) - 0.5f); }
                return result;
            }
        }
    }
}
