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

using Facebook.WitAi.Events;
using UnityEngine.Events;

namespace Facebook.WitAi.Interfaces
{
    public interface ITranscriptionProvider
    {
        /// <summary>
        /// Provides the last transcription value (could be a partial transcription)
        /// </summary>
        string LastTranscription { get; }

        /// <summary>
        /// Callback used to notify Wit subscribers of a partial transcription.
        /// </summary>
        WitTranscriptionEvent OnPartialTranscription { get; }

        /// <summary>
        /// Callback used to notify Wit subscribers of a full transcription
        /// </summary>
        WitTranscriptionEvent OnFullTranscription { get; }

        /// <summary>
        /// Callback used to notify Wit subscribers when the mic is active and transcription has begun
        /// </summary>
        UnityEvent OnStoppedListening { get; }

        /// <summary>
        /// Callback used to notify Wit subscribers when the mic is inactive and transcription has stopped
        /// </summary>
        UnityEvent OnStartListening { get; }

        /// <summary>
        /// Callback used to notify Wit subscribers on mic volume level changes
        /// </summary>
        WitMicLevelChangedEvent OnMicLevelChanged { get; }

        /// <summary>
        /// Tells Wit if the mic input levels from the transcription service should be used directly
        /// </summary>
        bool OverrideMicLevel { get; }

        /// <summary>
        /// Called when wit is activated
        /// </summary>
        void Activate();

        /// <summary>
        /// Called when
        /// </summary>
        void Deactivate();
    }
}
