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

using System.Text.RegularExpressions;
using Facebook.WitAi.Lib;
using Facebook.WitAi.Utilities;
using UnityEngine;

namespace Facebook.WitAi.CallbackHandlers
{
    [AddComponentMenu("Wit.ai/Response Matchers/Utterance Matcher")]
    public class WitUtteranceMatcher : WitResponseHandler
    {
        [SerializeField] private string searchText;
        [SerializeField] private bool exactMatch = true;
        [SerializeField] private bool useRegex;

        [SerializeField] private StringEvent onUtteranceMatched = new StringEvent();

        private Regex regex;

        protected override void OnHandleResponse(WitResponseNode response)
        {
            var text = response["text"].Value;

            if (useRegex)
            {
                if (null == regex)
                {
                    regex = new Regex(searchText, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                }

                var match = regex.Match(text);
                if (match.Success)
                {
                    if (exactMatch && match.Value == text)
                    {
                        onUtteranceMatched?.Invoke(text);
                    }
                    else
                    {
                        onUtteranceMatched?.Invoke(text);
                    }
                }
            }
            else if (exactMatch && text.ToLower() == searchText.ToLower())
            {
                onUtteranceMatched?.Invoke(text);
            }
            else if (text.ToLower().Contains(searchText.ToLower()))
            {
                onUtteranceMatched?.Invoke(text);
            }
        }
    }
}
