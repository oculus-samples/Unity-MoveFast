// Copyright (c) Meta Platforms, Inc. and affiliates.

using Oculus.Interaction.Throw;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Tracks the hand's velocity
    /// </summary>
    public class RawHandVelocity : MonoBehaviour, ISerializationCallbackReceiver
    {
        [SerializeField, Interface(typeof(IPoseInputDevice))]
        private MonoBehaviour _input;
        public IPoseInputDevice Input { get; private set; }

        Vector3[] _samples = new Vector3[12];
        Vector3 _lastPosition;
        int _writeIndex = 0;

        protected virtual void LateUpdate()
        {
            Pose referencePose;
            if (!Input.GetRootPose(out referencePose))
            {
                referencePose = new Pose(_lastPosition, Quaternion.identity);
            }

            _samples[_writeIndex] = (referencePose.position - _lastPosition) / Time.deltaTime;
            _writeIndex = (_writeIndex + 1) % _samples.Length;
            _lastPosition = referencePose.position;
        }

        internal ReleaseVelocityInformation CalculateThrowVelocity(Transform transform)
        {
            return new ReleaseVelocityInformation() { LinearVelocity = GetVelocity() };
        }

        public Vector3 GetVelocity()
        {
            var vel = _samples[0];
            for (int i = 1; i < _samples.Length; i++)
            {
                vel += _samples[i];
            }
            vel /= _samples.Length;
            if (vel.sqrMagnitude > 6.8f * 6.8f)
            {
                vel = vel.normalized * 6.8f;
            }
            return vel;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() => Input = _input as IPoseInputDevice;
        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
    }
}
