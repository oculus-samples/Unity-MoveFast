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
using Facebook.WitAi.Events;
using Facebook.WitAi.Interfaces;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Facebook.WitAi.Dictation.Events
{
    [Serializable]
    public class DictationEvents : EventRegistry, ITranscriptionEvent, IAudioInputEvents
    {
        private const string EVENT_CATEGORY_TRANSCRIPTION_EVENTS = "Transcription Events";
        private const string EVENT_CATEGORY_MIC_EVENTS = "Mic Events";
        private const string EVENT_CATEGORY_DICTATION_EVENTS = "Dictation Events";
        private const string EVENT_CATEGORY_ACTIVATION_RESULT_EVENTS = "Activation Result Events";

        [EventCategory(EVENT_CATEGORY_TRANSCRIPTION_EVENTS)]
        [FormerlySerializedAs("OnPartialTranscription")]
        [Tooltip("Message fired when a partial transcription has been received.")]
        public WitTranscriptionEvent onPartialTranscription = new WitTranscriptionEvent();

        [EventCategory(EVENT_CATEGORY_TRANSCRIPTION_EVENTS)]
        [FormerlySerializedAs("OnFullTranscription")]
        [Tooltip("Message received when a complete transcription is received.")]
        public WitTranscriptionEvent onFullTranscription = new WitTranscriptionEvent();

        [EventCategory(EVENT_CATEGORY_ACTIVATION_RESULT_EVENTS)]
        [Tooltip("Called when a response from Wit.ai has been received")]
        public WitResponseEvent onResponse = new WitResponseEvent();

        [EventCategory(EVENT_CATEGORY_ACTIVATION_RESULT_EVENTS)]
        public UnityEvent onStart = new UnityEvent();

        [EventCategory(EVENT_CATEGORY_ACTIVATION_RESULT_EVENTS)]
        public UnityEvent onStopped = new UnityEvent();

        [EventCategory(EVENT_CATEGORY_ACTIVATION_RESULT_EVENTS)]
        public WitErrorEvent onError = new WitErrorEvent();

        [EventCategory(EVENT_CATEGORY_DICTATION_EVENTS)]
        public DictationSessionEvent onDictationSessionStarted = new DictationSessionEvent();

        [EventCategory(EVENT_CATEGORY_DICTATION_EVENTS)]
        public DictationSessionEvent onDictationSessionStopped = new DictationSessionEvent();

        [EventCategory(EVENT_CATEGORY_MIC_EVENTS)]
        public WitMicLevelChangedEvent onMicAudioLevel = new WitMicLevelChangedEvent();

        #region Shared Event API - Transcription

        public WitTranscriptionEvent OnPartialTranscription => onPartialTranscription;
        public WitTranscriptionEvent OnFullTranscription => onFullTranscription;

        #endregion

        #region Shared Event API - Microphone

        public WitMicLevelChangedEvent OnMicAudioLevelChanged => onMicAudioLevel;
        public UnityEvent OnMicStartedListening => onStart;
        public UnityEvent OnMicStoppedListening => onStopped;

        #endregion
    }
}
