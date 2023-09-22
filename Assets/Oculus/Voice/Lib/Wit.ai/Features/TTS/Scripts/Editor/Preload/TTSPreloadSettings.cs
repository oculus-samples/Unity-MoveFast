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

namespace Facebook.WitAi.TTS.Editor.Preload
{
    [Serializable]
    public class TTSPreloadPhraseData
    {
        /// <summary>
        /// ID used to identify this phrase
        /// </summary>
        public string clipID;
        /// <summary>
        /// Actual phrase to be spoken
        /// </summary>
        public string textToSpeak;

        /// <summary>
        /// Meta data for whether clip is downloaded or not
        /// </summary>
        public bool downloaded;
        /// <summary>
        /// Meta data for clip download progress
        /// </summary>
        public float downloadProgress;
    }

    [Serializable]
    public class TTSPreloadVoiceData
    {
        /// <summary>
        /// Specific preset voice settings id to be used with TTSService
        /// </summary>
        public string presetVoiceID;
        /// <summary>
        /// All data corresponding to text to speak
        /// </summary>
        public TTSPreloadPhraseData[] phrases;
    }

    [Serializable]
    public class TTSPreloadData
    {
        public TTSPreloadVoiceData[] voices;
    }

    public class TTSPreloadSettings : ScriptableObject
    {
        [SerializeField] public TTSPreloadData data = new TTSPreloadData();
    }
}
