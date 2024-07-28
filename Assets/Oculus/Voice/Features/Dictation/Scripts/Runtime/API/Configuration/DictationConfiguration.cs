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
using UnityEngine;

namespace Oculus.Voice.Dictation.Configuration
{
    [Serializable]
    public class DictationConfiguration
    {
        [Tooltip("Re-open the mic after the final transcription. Useful for long form content/messaging.")]
        public bool multiPhrase;
        [Tooltip("Hint about the scenario that the user is dictating. Default to package name. In the future we might have messaging, search, general, etc")]
        public string scenario = "default";
        [Tooltip("Input types: text_default: Normal text, numeric: Numbers, email: Email addresses")]
        public string inputType = "text_default";
    }
}
