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

namespace Facebook.WitAi.TTS.Events
{
    [Serializable]
    public class TTSServiceEvents
    {
        [Tooltip("Called when a audio clip has been added to the runtime cache")]
        public TTSClipEvent OnClipCreated  = new TTSClipEvent();

        [Tooltip("Called when a audio clip has been removed from the runtime cache")]
        public TTSClipEvent OnClipUnloaded  = new TTSClipEvent();

        // Streaming events
        public TTSStreamEvents Stream = new TTSStreamEvents();

        // Download events
        public TTSDownloadEvents Download = new TTSDownloadEvents();
    }
}
