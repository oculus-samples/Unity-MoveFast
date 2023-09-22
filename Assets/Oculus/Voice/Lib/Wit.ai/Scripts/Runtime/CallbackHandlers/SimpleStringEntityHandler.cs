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

using System;
using Facebook.WitAi.Lib;
using UnityEngine;
using UnityEngine.Events;

namespace Facebook.WitAi.CallbackHandlers
{
    [AddComponentMenu("Wit.ai/Response Matchers/Simple String Entity Handler")]
    public class SimpleStringEntityHandler : WitResponseHandler
    {
        [SerializeField] public string intent;
        [SerializeField] public string entity;
        [Range(0, 1f)] [SerializeField] public float confidence = .9f;

        [SerializeField] public string format;

        [SerializeField] private StringEntityMatchEvent onIntentEntityTriggered
            = new StringEntityMatchEvent();

        public StringEntityMatchEvent OnIntentEntityTriggered => onIntentEntityTriggered;

        protected override void OnHandleResponse(WitResponseNode response)
        {
            var intentNode = WitResultUtilities.GetFirstIntent(response);
            if (intent == intentNode["name"].Value && intentNode["confidence"].AsFloat > confidence)
            {
                var entityValue = WitResultUtilities.GetFirstEntityValue(response, entity);
                if (!string.IsNullOrEmpty(format))
                {
                    onIntentEntityTriggered.Invoke(format.Replace("{value}", entityValue));
                }
                else
                {
                    onIntentEntityTriggered.Invoke(entityValue);
                }
            }
        }
    }

    [Serializable]
    public class StringEntityMatchEvent : UnityEvent<string> {}
}
