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

using Facebook.WitAi.TTS.Data;
using Facebook.WitAi.TTS.Events;

namespace Facebook.WitAi.TTS.Interfaces
{
    public interface ITTSRuntimeCacheHandler
    {
        /// <summary>
        /// Callback for clips being added to the runtime cache
        /// </summary>
        TTSClipEvent OnClipAdded { get; set; }
        /// <summary>
        /// Callback for clips being removed from the runtime cache
        /// </summary>
        TTSClipEvent OnClipRemoved { get; set; }

        /// <summary>
        /// Method for obtaining all cached clips
        /// </summary>
        TTSClipData[] GetClips();
        /// <summary>
        /// Method for obtaining a specific cached clip
        /// </summary>
        TTSClipData GetClip(string clipID);

        /// <summary>
        /// Method for adding a clip to the cache
        /// </summary>
        void AddClip(TTSClipData clipData);
        /// <summary>
        /// Method for removing a clip from the cache
        /// </summary>
        void RemoveClip(string clipID);
    }
}
