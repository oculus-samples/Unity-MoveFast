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
using Facebook.WitAi.Events.UnityEventListeners;
using Facebook.WitAi.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Facebook.WitAi.ServiceReferences
{
    /// <summary>
    /// Finds all audio event listeners in the scene and subscribes to them.
    /// This is good for creating generic attention systems that are shown for
    /// the same way for any voice based service active in the scene.
    /// </summary>
    //[Tooltip("Finds all voice based services and listens for changes in their audio input state.")]
    public class CombinedAudioEventReference : AudioInputServiceReference, IAudioInputEvents
    {
        public override IAudioInputEvents AudioEvents => this;

        private WitMicLevelChangedEvent _onMicAudioLevelChanged = new WitMicLevelChangedEvent();
        private UnityEvent _onMicStartedListening = new UnityEvent();
        private UnityEvent _onMicStoppedListening = new UnityEvent();
        private AudioEventListener[] _sourceListeners;

        private void Awake()
        {
            #if UNITY_2020_1_OR_NEWER
            _sourceListeners = FindObjectsOfType<AudioEventListener>(true);
            #else
            _sourceListeners = FindObjectsOfType<AudioEventListener>();
            #endif
        }

        private void OnEnable()
        {
            foreach (var listener in _sourceListeners)
            {
                listener.OnMicAudioLevelChanged.AddListener(OnMicAudioLevelChanged.Invoke);
                listener.OnMicStartedListening.AddListener(OnMicStartedListening.Invoke);
                listener.OnMicStoppedListening.AddListener(OnMicStoppedListening.Invoke);
            }
        }

        private void OnDisable()
        {
            foreach (var listener in _sourceListeners)
            {
                listener.OnMicAudioLevelChanged.RemoveListener(OnMicAudioLevelChanged.Invoke);
                listener.OnMicStartedListening.RemoveListener(OnMicStartedListening.Invoke);
                listener.OnMicStoppedListening.RemoveListener(OnMicStoppedListening.Invoke);
            }
        }

        public WitMicLevelChangedEvent OnMicAudioLevelChanged => _onMicAudioLevelChanged;
        public UnityEvent OnMicStartedListening => _onMicStartedListening;
        public UnityEvent OnMicStoppedListening => _onMicStoppedListening;
    }
}
