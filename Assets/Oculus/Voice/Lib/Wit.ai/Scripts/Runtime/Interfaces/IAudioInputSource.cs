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
using Facebook.WitAi.Data;

namespace Facebook.WitAi.Interfaces
{
    public interface IAudioInputSource
    {
        /// <summary>
        /// Invoked when the instance starts Recording.
        /// </summary>
        event Action OnStartRecording;

        /// <summary>
        /// Invoked when an AudioClip couldn't be created to start recording.
        /// </summary>
        event Action OnStartRecordingFailed;

        /// <summary>
        /// Invoked everytime an audio frame is collected. Includes the frame.
        /// </summary>
        event Action<int, float[], float> OnSampleReady;

        /// <summary>
        /// Invoked when the instance stop Recording.
        /// </summary>
        event Action OnStopRecording;

        void StartRecording(int sampleLen);

        void StopRecording();

        bool IsRecording { get; }

        /// <summary>
        /// Settings determining how audio is encoded by the source.
        ///
        /// NOTE: Default values for AudioEncoding are server optimized to reduce latency.
        /// </summary>
        AudioEncoding AudioEncoding { get; }

        /// <summary>
        /// Return true if input is available.
        /// </summary>
        bool IsInputAvailable { get; }

        /// <summary>
        /// Checks for input
        /// </summary>
        void CheckForInput();
    }
}
