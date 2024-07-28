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

namespace Oculus.Voice.Dictation.Listeners
{
    public interface DictationListener
    {
        /// <summary>
        /// Called when dictation has started
        /// </summary>
        void OnStart(DictationListener listener);

        /// <summary>
        /// Called when mic level changes. Used for building UI.
        /// </summary>
        /// <param name="micLevel"></param>
        void OnMicAudioLevel(float micLevel);

        /// <summary>
        /// Called with current predicted transcription. Could change as user speaks.
        /// </summary>
        /// <param name="transcription"></param>
        void OnPartialTranscription(string transcription);

        /// <summary>
        /// Final transcription of what the user has said
        /// </summary>
        /// <param name="transcription"></param>
        void OnFinalTranscription(string transcription);

        /// <summary>
        /// Called when there was an error with the dictation service
        /// </summary>
        /// <param name="errorType">The type of error encountered</param>
        /// <param name="errorMessage">Human readable message describing the error</param>
        void OnError(string errorType, string errorMessage);

        /// <summary>
        /// Called when the dictation session is done
        /// </summary>
        void OnStopped(DictationListener listener);
    }
}
