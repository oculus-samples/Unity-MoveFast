// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Applies the hands velocity to a spawned object
    /// </summary>
    public class SpawnerInheritHandHitVelocity : MonoBehaviour, ISpawnerModifier
    {
        [SerializeField]
        private HandHitDetector _hitDetector;
        [SerializeField]
        private bool _rigidbody = true;
        [SerializeField]
        private bool _orientation = false;

        public void Modify(GameObject instance)
        {
            if (!_hitDetector.LastHand.TryGetAspect<RawHandVelocity>(out var velocityCalculator))
            {
                return;
            }

            var velocity = velocityCalculator.CalculateThrowVelocity(instance.transform);

            if (_rigidbody && instance.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                rigidbody.velocity = velocity.LinearVelocity;
            }

            if (_orientation)
            {
                instance.transform.rotation = Quaternion.LookRotation(velocity.LinearVelocity);
            }
        }
    }
}
