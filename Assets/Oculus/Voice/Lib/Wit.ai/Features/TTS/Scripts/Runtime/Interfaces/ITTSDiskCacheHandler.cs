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
    public interface ITTSDiskCacheHandler
    {
        /// <summary>
        /// All events for streaming from the disk cache
        /// </summary>
        TTSStreamEvents DiskStreamEvents { get; set; }

        /// <summary>
        /// The default cache settings
        /// </summary>
        TTSDiskCacheSettings DiskCacheDefaultSettings { get; }

        /// <summary>
        /// A method for obtaining the path to a specific cache clip
        /// </summary>
        /// <param name="clipData">Clip request data</param>
        /// <returns>Returns the clip's cache path</returns>
        string GetDiskCachePath(TTSClipData clipData);

        /// <summary>
        /// Whether or not the clip data should be cached on disk
        /// </summary>
        /// <param name="clipData">Clip request data</param>
        /// <returns>Returns true if should cache</returns>
        bool ShouldCacheToDisk(TTSClipData clipData);

        /// <summary>
        /// Performs a check to determine if a file is cached to disk or not
        /// </summary>
        /// <param name="clipData">Clip request data</param>
        /// <returns>Returns true if currently on disk (Except for Android Streaming Assets)</returns>
        bool IsCachedToDisk(TTSClipData clipData);

        /// <summary>
        /// Method for streaming from disk cache
        /// </summary>
        /// <param name="clipData">Clip request data</param>
        void StreamFromDiskCache(TTSClipData clipData);

        /// <summary>
        /// Method for cancelling a running cache load request
        /// </summary>
        /// <param name="clipData">Clip request data</param>
        void CancelDiskCacheStream(TTSClipData clipData);
    }
}
