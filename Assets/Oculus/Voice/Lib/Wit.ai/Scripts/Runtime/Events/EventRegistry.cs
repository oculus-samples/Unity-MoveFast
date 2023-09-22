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

namespace Facebook.WitAi.Events
{
    public class EventRegistry
    {
        [SerializeField]
        private List<string> _overriddenCallbacks = new List<string>();
        private HashSet<string> _overriddenCallbacksHash;

        public HashSet<string> OverriddenCallbacks
        {
            get
            {
                if (_overriddenCallbacksHash == null)
                {
                    _overriddenCallbacksHash = new HashSet<string>(_overriddenCallbacks);
                }

                return _overriddenCallbacksHash;
            }
        }

        public void RegisterOverriddenCallback(string callback)
        {
            if (!_overriddenCallbacks.Contains(callback))
            {
                _overriddenCallbacks.Add(callback);
                _overriddenCallbacksHash.Add(callback);
            }
        }

        public void RemoveOverriddenCallback(string callback)
        {
            if (_overriddenCallbacks.Contains(callback))
            {
                _overriddenCallbacks.Remove(callback);
                _overriddenCallbacksHash.Remove(callback);
            }
        }

        public bool IsCallbackOverridden(string callback)
        {
            return OverriddenCallbacks.Contains(callback);
        }
    }
}
