// Copyright (c) Meta Platforms, Inc. and affiliates.



using Oculus.Interaction.Input;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Oculus.Interaction.MoveFast
{
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
        //public bool GetCenterEyePose(out Pose pose) => hand.GetCenterEyePose(out pose);
        //public bool TryGetAspect<TComponent>(out TComponent foundComponent) where TComponent : class => hand.TryGetAspect(out foundComponent);

    }

#if UNITY_EDITOR
    /// <summary>Renders the struct inline</summary>
    [UnityEditor.CustomPropertyDrawer(typeof(IHandReference))]
    class HandReferenceDrawer : InlineDrawer {}
#endif
}
