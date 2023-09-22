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

using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// A list of named IActiveStates
    /// </summary>
    public class HandPoseActiveStateList : MonoBehaviour
    {
        [SerializeField, Interface(typeof(IActiveState))]
        List<MonoBehaviour> _poses;

        internal IActiveState Get(string poseName)
        {
            return _poses.Find(x => x.name == poseName) as IActiveState;
        }

        public int ActiveCount()
        {
            int count = 0;
            foreach (var pose in _poses)
            {
                if ((pose as IActiveState).Active) count++;
            }
            return count;
        }

        private void Awake()
        {
            _poses.ForEach(x => _activeStates.Add(x.name, new DelayedFalseActiveState(x as IActiveState, 0.05f)));
        }

        private void Update()
        {
            foreach (var x in _activeStates) x.Value.Update();
        }

        private Dictionary<string, DelayedFalseActiveState> _activeStates = new Dictionary<string, DelayedFalseActiveState>();

        class DelayedFalseActiveState : IActiveState
        {
            private float _delay = 0.1f;
            private IActiveState _activeState;
            private float _lastActiveTime;

            public DelayedFalseActiveState(IActiveState activeState, float delay)
            {
                _activeState = activeState;
                _lastActiveTime = -1;
                _delay = delay;
                Update();
            }

            public bool Active
            {
                get
                {
                    Update();
                    return Time.time - _lastActiveTime < _delay;
                }
            }

            internal void Update()
            {
                if (_activeState.Active) _lastActiveTime = Time.time;
            }
        }
    }
}
