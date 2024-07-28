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
    /// Updates one string property in response to another
    /// e.g. to switch to a default sub page when a page is opened
    /// </summary>
    public class StringPropertySync : ActiveStateObserver
    {
        [SerializeField]
        StringPropertyRef _property;

        [SerializeField]
        StateChange _becameActive;

        [SerializeField]
        StateChange _becameInactive;

        protected override void HandleActiveStateChanged()
        {
            if (Active && _becameActive.enabled)
            {
                _property.Value = _becameActive.value;
            }
            else if (!Active && _becameInactive.enabled)
            {
                _property.Value = _becameInactive.value;
            }
        }

        [System.Serializable]
        struct StateChange
        {
            public bool enabled;
            public string value;
        }
    }
}
