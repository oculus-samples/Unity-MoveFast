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
using Facebook.WitAi.TTS.Data;

namespace Facebook.WitAi.TTS.Interfaces
{
    public interface ITTSVoiceProvider
    {
        /// <summary>
        /// Returns preset voice data if no voice data is selected.
        /// Useful for menu ai, etc.
        /// </summary>
        TTSVoiceSettings VoiceDefaultSettings { get; }

        /// <summary>
        /// Returns all preset voice settings
        /// </summary>
        TTSVoiceSettings[] PresetVoiceSettings { get; }

        /// <summary>
        /// Encode voice data to be transmitted
        /// </summary>
        /// <param name="voiceSettings">The voice settings class</param>
        /// <returns>Returns a dictionary with all variables</returns>
        Dictionary<string, string> EncodeVoiceSettings(TTSVoiceSettings voiceSettings);
    }
}
