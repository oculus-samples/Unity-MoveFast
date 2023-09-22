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
using UnityEngine;
using UnityEngine.Events;

namespace Facebook.WitAi.Interfaces
{
    public abstract class CustomTranscriptionProvider : MonoBehaviour, ITranscriptionProvider
    {
        [SerializeField] private bool overrideMicLevel = false;

        private WitTranscriptionEvent onPartialTranscription = new WitTranscriptionEvent();
        private WitTranscriptionEvent onFullTranscription = new WitTranscriptionEvent();
        private UnityEvent onStoppedListening = new UnityEvent();
        private UnityEvent onStartListening = new UnityEvent();
        private WitMicLevelChangedEvent onMicLevelChanged = new WitMicLevelChangedEvent();

        public string LastTranscription { get; }
        public WitTranscriptionEvent OnPartialTranscription => onPartialTranscription;
        public WitTranscriptionEvent OnFullTranscription => onFullTranscription;
        public UnityEvent OnStoppedListening => onStoppedListening;
        public UnityEvent OnStartListening => onStartListening;
        public WitMicLevelChangedEvent OnMicLevelChanged => onMicLevelChanged;
        public bool OverrideMicLevel => overrideMicLevel;

        public abstract void Activate();
        public abstract void Deactivate();
    }
}
