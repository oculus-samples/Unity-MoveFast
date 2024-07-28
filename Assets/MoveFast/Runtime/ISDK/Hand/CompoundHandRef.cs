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

using Oculus.Interaction.Input;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Wraps several hands (e.g. controller and tracked) and picks the first active one
    /// Used as a 'master' hand, allowing HandRefs to reference a single IHand that could be the controller or tracked hand
    /// </summary>
    public class CompoundHandRef : MonoBehaviour, IHand
    {
        [SerializeField]
        private List<IHandReference> _hands;
        public readonly List<IHand> Hands = new List<IHand>();

        [SerializeField]
        private Component[] _aspects = new Component[0];

        Action _whenHandUpdated;

        private void Awake()
        {
            Hands.AddRange(_hands.ConvertAll(x => x.Hand));
            Hands.ForEach(x => x.WhenHandUpdated += () => InvokeWhenHandUpdated(x));
        }

        private void InvokeWhenHandUpdated(IHand x)
        {
            if (x == BestHand)
            {
                _whenHandUpdated?.Invoke();
            }
        }

        private IHand BestHand => Hands.Find(x => x.IsConnected) ?? NullHand.instance;

        public bool TryGetAspect<TComponent>(out TComponent foundComponent) where TComponent : class
        {
            for (int i = 0; i < _aspects.Length; i++)
            {
                foundComponent = _aspects[i] as TComponent;
                if (foundComponent != null)
                {
                    return true;
                }
            }

            if (BestHand.TryGetAspect(out foundComponent))
            {
                return true;
            }

            for (int i = 0; i < Hands.Count; i++)
            {
                if (Hands[i] != BestHand && Hands[i].TryGetAspect(out foundComponent))
                {
                    return true;
                }
            }

            return false;
        }

        public event Action WhenHandUpdated
        {
            add { _whenHandUpdated += value; }
            remove { _whenHandUpdated -= value; }
        }

        public Handedness Handedness => BestHand.Handedness;
        public bool IsConnected => BestHand.IsConnected;
        public bool IsHighConfidence => BestHand.IsHighConfidence;
        public bool IsDominantHand => BestHand.IsDominantHand;
        public float Scale => BestHand.Scale;
        public bool IsPointerPoseValid => BestHand.IsPointerPoseValid;
        public bool IsTrackedDataValid => BestHand.IsTrackedDataValid;
        public bool IsCenterEyePoseValid => BestHand.IsCenterEyePoseValid;
        public Transform TrackingToWorldSpace => BestHand.TrackingToWorldSpace;
        public int CurrentDataVersion => BestHand.CurrentDataVersion;
        public bool GetCenterEyePose(out Pose pose) => BestHand.GetCenterEyePose(out pose);
        public bool GetFingerIsHighConfidence(HandFinger finger) => BestHand.GetFingerIsHighConfidence(finger);
        public bool GetFingerIsPinching(HandFinger finger) => BestHand.GetFingerIsPinching(finger);
        public float GetFingerPinchStrength(HandFinger finger) => BestHand.GetFingerPinchStrength(finger);
        public bool GetIndexFingerIsPinching() => BestHand.GetIndexFingerIsPinching();
        public bool GetJointPose(HandJointId handJointId, out Pose pose) => BestHand.GetJointPose(handJointId, out pose);
        public bool GetJointPoseFromWrist(HandJointId handJointId, out Pose pose) => BestHand.GetJointPoseFromWrist(handJointId, out pose);
        public bool GetJointPoseLocal(HandJointId handJointId, out Pose pose) => BestHand.GetJointPoseLocal(handJointId, out pose);
        public bool GetJointPosesFromWrist(out ReadOnlyHandJointPoses jointPosesFromWrist) => BestHand.GetJointPosesFromWrist(out jointPosesFromWrist);
        public bool GetJointPosesLocal(out ReadOnlyHandJointPoses localJointPoses) => BestHand.GetJointPosesLocal(out localJointPoses);
        public bool GetPalmPoseLocal(out Pose pose) => BestHand.GetPalmPoseLocal(out pose);
        public bool GetPointerPose(out Pose pose) => BestHand.GetPointerPose(out pose);
        public bool GetRootPose(out Pose pose) => BestHand.GetRootPose(out pose);
    }

    /// <summary>
    /// Serialized reference to an IHand, that also implements IHand through the reference
    /// Could be swapped for Interface(typeof(IHand)) and a Monobehaviour, then assigning it in Awake and checking it in Start
    /// </summary>
    [Serializable]
    public struct IHandReference : ISerializationCallbackReceiver, IHand
    {
        [SerializeField, Interface(typeof(IHand))]
        private MonoBehaviour _hand;
        private IHand hand;

        public IHand Hand
        {
            get => hand;
            set
            {
                hand = value;
                _hand = value as MonoBehaviour;
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() => hand = _hand as IHand;
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        public void AssertNotNull(string message = default) => Assert.IsNotNull(hand, message);

        public Handedness Handedness => hand.Handedness;
        public bool IsConnected => hand.IsConnected;
        public bool IsHighConfidence => hand.IsHighConfidence;
        public bool IsDominantHand => hand.IsDominantHand;
        public float Scale => hand.Scale;
        public bool IsPointerPoseValid => hand.IsPointerPoseValid;
        public bool IsTrackedDataValid => hand.IsTrackedDataValid;
        public bool IsCenterEyePoseValid => hand.IsCenterEyePoseValid;
        public Transform TrackingToWorldSpace => hand.TrackingToWorldSpace;
        public int CurrentDataVersion => hand.CurrentDataVersion;

        public event Action WhenHandUpdated
        {
            add => hand.WhenHandUpdated += value;
            remove => hand.WhenHandUpdated -= value;
        }

        public bool GetFingerIsPinching(HandFinger finger) => hand.GetFingerIsPinching(finger);
        public bool GetIndexFingerIsPinching() => hand.GetIndexFingerIsPinching();
        public bool GetPointerPose(out Pose pose) => hand.GetPointerPose(out pose);
        public bool GetJointPose(HandJointId handJointId, out Pose pose) => hand.GetJointPose(handJointId, out pose);
        public bool GetJointPoseLocal(HandJointId handJointId, out Pose pose) => hand.GetJointPoseLocal(handJointId, out pose);
        public bool GetJointPosesLocal(out ReadOnlyHandJointPoses localJointPoses) => hand.GetJointPosesLocal(out localJointPoses);
        public bool GetJointPoseFromWrist(HandJointId handJointId, out Pose pose) => hand.GetJointPoseFromWrist(handJointId, out pose);
        public bool GetJointPosesFromWrist(out ReadOnlyHandJointPoses jointPosesFromWrist) => hand.GetJointPosesFromWrist(out jointPosesFromWrist);
        public bool GetPalmPoseLocal(out Pose pose) => hand.GetPalmPoseLocal(out pose);
        public bool GetFingerIsHighConfidence(HandFinger finger) => hand.GetFingerIsHighConfidence(finger);
        public float GetFingerPinchStrength(HandFinger finger) => hand.GetFingerPinchStrength(finger);
        public bool GetRootPose(out Pose pose) => hand.GetRootPose(out pose);
        public bool GetCenterEyePose(out Pose pose) => hand.GetCenterEyePose(out pose);
        public bool TryGetAspect<TAspect>(out TAspect aspect) where TAspect : class => hand.TryGetAspect(out aspect);
    }

#if UNITY_EDITOR
    /// <summary>Renders the struct inline</summary>
    [UnityEditor.CustomPropertyDrawer(typeof(IHandReference))]
    class HandReferenceDrawer : UnityEditor.PropertyDrawer
    {
        public override float GetPropertyHeight(UnityEditor.SerializedProperty _, GUIContent __) => UnityEditor.EditorGUIUtility.singleLineHeight;
        public override void OnGUI(Rect rect, UnityEditor.SerializedProperty prop, GUIContent label) => UnityEditor.EditorGUI.PropertyField(rect, prop.FindPropertyRelative("_hand"), label);
    }
#endif

    /// <summary>
    /// IHand that does nothing, can be used to prevent null reference exceptions
    /// </summary>
    public class NullHand : IHand
    {
        public static readonly NullHand instance = new NullHand();
        public Handedness Handedness => Handedness.Left;
        public bool IsConnected => false;
        public bool IsHighConfidence => false;
        public bool IsDominantHand => false;
        public float Scale => 1f;
        public bool IsPointerPoseValid => false;
        public bool IsTrackedDataValid => false;
        public bool IsCenterEyePoseValid => false;
        public Transform TrackingToWorldSpace => null;
        public int CurrentDataVersion => 0;
        public event Action WhenHandUpdated = delegate { };
        public bool GetFingerIsHighConfidence(HandFinger finger) => false;
        public bool GetFingerIsPinching(HandFinger finger) => false;
        public float GetFingerPinchStrength(HandFinger finger) => 0f;
        public bool GetIndexFingerIsPinching() => false;

        public bool GetCenterEyePose(out Pose pose)
        {
            pose = Pose.identity;
            return false;
        }
        public bool GetHandAspect<TComponent>(out TComponent foundComponent) where TComponent : class
        {
            foundComponent = null;
            return false;
        }
        public bool GetJointPose(HandJointId handJointId, out Pose pose)
        {
            pose = Pose.identity;
            return false;
        }
        public bool GetJointPoseFromWrist(HandJointId handJointId, out Pose pose)
        {
            pose = Pose.identity;
            return false;
        }
        public bool GetJointPoseLocal(HandJointId handJointId, out Pose pose)
        {
            pose = Pose.identity;
            return false;
        }
        public bool GetJointPosesFromWrist(out ReadOnlyHandJointPoses jointPosesFromWrist)
        {
            jointPosesFromWrist = null;
            return false;
        }
        public bool GetJointPosesLocal(out ReadOnlyHandJointPoses localJointPoses)
        {
            localJointPoses = null;
            return false;
        }
        public bool GetPalmPoseLocal(out Pose pose)
        {
            pose = Pose.identity;
            return false;
        }
        public bool GetPointerPose(out Pose pose)
        {
            pose = Pose.identity;
            return false;
        }
        public bool GetRootPose(out Pose pose)
        {
            pose = Pose.identity;
            return false;
        }

        public bool TryGetAspect<TAspect>(out TAspect aspect) where TAspect : class
        {
            aspect = null;
            return false;
        }
    }
}
