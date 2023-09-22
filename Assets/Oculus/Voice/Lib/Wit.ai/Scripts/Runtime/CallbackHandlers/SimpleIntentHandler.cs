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
using UnityEngine.Events;

namespace Facebook.WitAi.CallbackHandlers
{
    [AddComponentMenu("Wit.ai/Response Matchers/Simple Intent Handler")]
    public class SimpleIntentHandler : WitResponseHandler
    {
        [SerializeField] public string intent;
        [Range(0, 1f)]
        [SerializeField] public float confidence = .9f;
        [SerializeField] private UnityEvent onIntentTriggered = new UnityEvent();

        [Tooltip("Confidence ranges are executed in order. If checked, all confidence values will be checked instead of stopping on the first one that matches.")]
        [SerializeField] public bool allowConfidenceOverlap;
        [SerializeField] public ConfidenceRange[] confidenceRanges;

        public UnityEvent OnIntentTriggered => onIntentTriggered;

        protected override void OnHandleResponse(WitResponseNode response)
        {
            if (null == response) return;

            bool matched = false;
            foreach (var intentNode in response?["intents"]?.Childs)
            {
                var resultConfidence = intentNode["confidence"].AsFloat;
                if (intent == intentNode["name"].Value)
                {
                    matched = true;
                    if (resultConfidence >= confidence)
                    {
                        onIntentTriggered.Invoke();
                    }

                    CheckInsideRange(resultConfidence);
                    CheckOutsideRange(resultConfidence);
                }
            }

            if(!matched)
            {
                CheckInsideRange(0);
                CheckOutsideRange(0);
            }
        }

        private void CheckOutsideRange(float resultConfidence)
        {
            for (int i = 0; null != confidenceRanges && i < confidenceRanges.Length; i++)
            {
                var range = confidenceRanges[i];
                if (resultConfidence < range.minConfidence ||
                    resultConfidence > range.maxConfidence)
                {
                    range.onOutsideConfidenceRange?.Invoke();

                    if (!allowConfidenceOverlap) break;
                }
            }
        }

        private void CheckInsideRange(float resultConfidence)
        {
            for (int i = 0; null != confidenceRanges && i < confidenceRanges.Length; i++)
            {
                var range = confidenceRanges[i];
                if (resultConfidence >= range.minConfidence &&
                    resultConfidence <= range.maxConfidence)
                {
                    range.onWithinConfidenceRange?.Invoke();

                    if (!allowConfidenceOverlap) break;
                }
            }
        }
    }
}
