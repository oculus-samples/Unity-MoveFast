// Copyright (c) Meta Platforms, Inc. and affiliates.

using Oculus.Interaction.Input;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Updates the position of this transform to the position provided by the IHand for the specified joint.
    /// If an offsetSource is provided it will used to generate a pose to offset the joint pose by.
    ///
    /// Use the offsetSource when working with a custom hand whose bones do not align with the Oculus hand;
    /// bring the oculus hand into the prefab, align the custom hand with the oculus hand and assign the
    /// appropriate joint transform as the offset source.
    /// </summary>
    public class JointTracker : MonoBehaviour
    {
        [SerializeField, Interface(typeof(IHand))]
        private MonoBehaviour _hand;
        public IHand Hand;

        [SerializeField]
        private Track _source;

        [SerializeField]
        private float _dragDistance = 0;

        private Pose _offset;

        private void Awake()
        {
            Hand = _hand as IHand;
            _offset = _source.GetOffset(transform);
        }

        private void Update()
        {
            Hand.GetJointPose(_source.joint, out var pose);
            if (Hand.Handedness == Handedness.Left)
            {
                pose.rotation = pose.rotation * Quaternion.Euler(0, 0, 180);
            }
            var offsetPose = _offset.GetTransformedBy(pose);

            if (_dragDistance > 0)
            {
                offsetPose.position = Vector3.MoveTowards(offsetPose.position, transform.position, _dragDistance);
            }

            transform.SetPose(offsetPose);
        }

        [System.Serializable]
        struct Track
        {
            public HandJointId joint;
            [Optional]
            public Transform offsetSource;

            public Pose GetOffset(Transform forTransform)
            {
                //TODO @Mark this Delta is from offsetSource to forTransform. Is that the right order?
                return offsetSource ? offsetSource.Delta(forTransform) : Pose.identity;
            }
        }
    }
}
