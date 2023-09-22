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
using Meta.Conduit;

namespace Facebook.WitAi
{
    /// <summary>
    /// Triggers a method to be executed if it matches a voice command's intent
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class MatchIntent : ConduitActionAttribute
    {
        /// <summary>
        /// Triggers a method to be executed if it matches a voice command's intent
        /// </summary>
        /// <param name="intent">The name of the intent to match</param>
        /// <param name="minConfidence">The minimum confidence value (0-1) needed to match</param>
        /// <param name="maxConfidence">The maximum confidence value(0-1) needed to match</param>
        public MatchIntent(string intent, float minConfidence = DEFAULT_MIN_CONFIDENCE, float maxConfidence = DEFAULT_MAX_CONFIDENCE) : base(intent, minConfidence, maxConfidence, false)
        {
        }
    }
}
