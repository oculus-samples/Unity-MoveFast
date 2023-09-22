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
    public class TTSClipEvent : UnityEvent<TTSClipData>
    {
    }
    [Serializable]
    public class TTSClipErrorEvent : UnityEvent<TTSClipData, string>
    {
    }

    [Serializable]
    public class TTSStreamEvents
    {
        [Tooltip("Called when a audio clip stream begins")]
        public TTSClipEvent OnStreamBegin = new TTSClipEvent();

        [Tooltip("Called when a audio clip is ready for playback")]
        public TTSClipEvent OnStreamReady = new TTSClipEvent();

        [Tooltip("Called when a audio clip stream has been cancelled")]
        public TTSClipEvent OnStreamCancel = new TTSClipEvent();

        [Tooltip("Called when a audio clip stream has failed")]
        public TTSClipErrorEvent OnStreamError = new TTSClipErrorEvent();
    }
}
