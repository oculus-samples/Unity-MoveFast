// Copyright (c) Meta Platforms, Inc. and affiliates.

using Oculus.Interaction.Input;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Determines if the Hand is in the right pose as it collides with the object
    /// </summary>
    public class HandHitDetector : MonoBehaviour
    {
        [SerializeField]
        private string _poseName;

        [SerializeField]
        float _minTimeBetweenHits = 0.6f;

        [SerializeField]
        int _poseFrameTolerance = 3;

        [SerializeField]
        ReferenceActiveState _canHit;

        public UnityEvent onHit;

        public event Action WhenHitResolved;

        private TriggerZoneList<IHand> _triggerZone;
        private float _lastHitTime;

        /// <summary>
        /// The hand that most recently hit this target
        /// </summary>
        public IHand LastHand { get; private set; }
        public bool PoseWasCorrect { get; private set; }

        public static bool TutorialMode = false;

        private void Start()
        {
            _triggerZone = new TriggerZoneList<IHand>(gameObject.AddComponent<TriggerZone>());
            _triggerZone.WhenAdded += TestForHitPose;
        }

        private void OnDestroy()
        {
            _triggerZone.Dispose();
            _triggerZone = null;
        }

        private void TestForHitPose(IHand hand)
        {
            if (!_canHit) return;
            if (Time.time - _lastHitTime < _minTimeBetweenHits) return;

            _lastHitTime = Time.time;
            LastHand = hand;

            if (TutorialMode)
            {
                TutorialHit(hand);
            }
            else
            {
                IngameHit(hand);
            }
        }

        /// <summary>
        /// Only hits if the hand is in the right pose, to teach users the pose
        /// </summary>
        /// <param name="hand"></param>
        private void TutorialHit(IHand hand)
        {
            var hasPoseList = hand.TryGetAspect<HandPoseActiveStateList>(out var handPoseList);

            var exclusive = _poseName != "block"; //HACK
            var activeCount = hasPoseList && exclusive ? handPoseList.ActiveCount() : 1;

            if (exclusive && activeCount > 1) return;

            if (!hasPoseList || handPoseList.Get(_poseName).Active)
            {
                onHit?.Invoke();
                ResolveHit(hand, true);
            }
        }

        /// <summary>
        /// Hits regardless to the pose, resolves true if the hand was in the right pose
        /// </summary>
        /// <param name="hand"></param>
        private void IngameHit(IHand hand)
        {
            onHit?.Invoke();

            bool hasPoseList = hand.TryGetAspect<HandPoseActiveStateList>(out var handPoseList);

            // hand poses are not available, probably a controller, just say it was posed right
            if (!hasPoseList)
            {
                ResolveHit(hand, true);
                return;
            }

            StartCoroutine(Tolerance());
            IEnumerator Tolerance()
            {
                var handPoseActiveState = handPoseList.Get(_poseName);
                if (handPoseActiveState != null)
                {
                    for (int i = 0; i < _poseFrameTolerance; i++)
                    {
                        // hand was in the right pose \o/
                        if (handPoseActiveState.Active)
                        {
                            ResolveHit(hand, true);
                            yield break;
                        }
                        yield return null;
                    }
                }
                ResolveHit(hand, false);
            }
        }

        void ResolveHit(IHand hand, bool poseCorrect)
        {
            PoseWasCorrect = poseCorrect;
            WhenHitResolved?.Invoke();
        }
    }
}
