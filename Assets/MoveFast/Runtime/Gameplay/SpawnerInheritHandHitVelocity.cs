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
