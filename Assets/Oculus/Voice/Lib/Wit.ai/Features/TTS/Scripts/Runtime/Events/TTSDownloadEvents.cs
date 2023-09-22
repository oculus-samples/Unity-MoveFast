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
using Facebook.WitAi.TTS.Data;
using UnityEngine;
using UnityEngine.Events;

namespace Facebook.WitAi.TTS.Events
{
    [Serializable]
    public class TTSClipDownloadEvent : UnityEvent<TTSClipData, string>
    {
    }
    [Serializable]
    public class TTSClipDownloadErrorEvent : UnityEvent<TTSClipData, string, string>
    {
    }

    [Serializable]
    public class TTSDownloadEvents
    {
        [Tooltip("Called when a audio clip download begins")]
        public TTSClipDownloadEvent OnDownloadBegin = new TTSClipDownloadEvent();

        [Tooltip("Called when a audio clip is downloaded successfully")]
        public TTSClipDownloadEvent OnDownloadSuccess = new TTSClipDownloadEvent();

        [Tooltip("Called when a audio clip downloaded has been cancelled")]
        public TTSClipDownloadEvent OnDownloadCancel = new TTSClipDownloadEvent();

        [Tooltip("Called when a audio clip downloaded has failed")]
        public TTSClipDownloadErrorEvent OnDownloadError = new TTSClipDownloadErrorEvent();
    }
}
