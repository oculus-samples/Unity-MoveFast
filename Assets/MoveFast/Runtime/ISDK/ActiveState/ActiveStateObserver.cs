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
    /// Checks for an ActiveState to have changed in Update
    /// Similar to ActiveStateSelector but using ReferenceActiveState and designed for inheritance
    /// </summary>
    public abstract class ActiveStateObserver : MonoBehaviour
    {
        [SerializeField]
        private ReferenceActiveState _activeState;

        protected bool Active { get; private set; }

        protected virtual void Reset()
        {
            _activeState.InjectActiveState(GetComponent<IActiveState>());
        }

        protected virtual void Start()
        {
            _activeState.AssertNotNull($"{name} ({GetType()}) requires an IActiveState assigned");
        }

        protected virtual void Update()
        {
            if (Active != _activeState.Active)
            {
                Active = !Active;
                HandleActiveStateChanged();
            }
        }

        protected abstract void HandleActiveStateChanged();

        #region Inject
        public void InjectActiveState(IActiveState activeState) => _activeState.InjectActiveState(activeState);
        #endregion
    }
}
