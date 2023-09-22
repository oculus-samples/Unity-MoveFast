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

using Facebook.WitAi.Lib;
using UnityEngine;

namespace Facebook.WitAi.CallbackHandlers
{
    public abstract class WitResponseHandler : MonoBehaviour
    {
        [SerializeField] public VoiceService wit;

        private void OnValidate()
        {
            if (!wit) wit = FindObjectOfType<VoiceService>();
        }

        private void OnEnable()
        {
            if (!wit) wit = FindObjectOfType<VoiceService>();
            if (!wit)
            {
                Debug.LogError("Wit not found in scene. Disabling " + GetType().Name + " on " +
                               name);
                enabled = false;
            }
            else
            {
                wit.events.OnResponse.AddListener(OnHandleResponse);
            }
        }

        private void OnDisable()
        {
            if (wit)
            {
                wit.events.OnResponse.RemoveListener(OnHandleResponse);
            }
        }

        public void HandleResponse(WitResponseNode response) => OnHandleResponse(response);
        protected abstract void OnHandleResponse(WitResponseNode response);
    }
}
