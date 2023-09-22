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

using Facebook.WitAi.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Facebook.WitAi.Events.UnityEventListeners
{
    [RequireComponent(typeof(IAudioEventProvider))]
    public class AudioEventListener : MonoBehaviour, IAudioInputEvents
    {
        [SerializeField] private WitMicLevelChangedEvent onMicAudioLevelChanged = new WitMicLevelChangedEvent();
        [SerializeField] private UnityEvent onMicStartedListening = new UnityEvent();
        [SerializeField] private UnityEvent onMicStoppedListening = new UnityEvent();

        public WitMicLevelChangedEvent OnMicAudioLevelChanged => onMicAudioLevelChanged;
        public UnityEvent OnMicStartedListening => onMicStartedListening;
        public UnityEvent OnMicStoppedListening => onMicStoppedListening;

        private IAudioInputEvents _events;

        private IAudioInputEvents AudioInputEvents
        {
            get
            {
                if (null == _events)
                {
                    var eventProvider = GetComponent<IAudioEventProvider>();
                    if (null != eventProvider)
                    {
                        _events = eventProvider.AudioEvents;
                    }
                }

                return _events;
            }
        }

        private void OnEnable()
        {
            var events = AudioInputEvents;
            if (null != events)
            {
                events.OnMicAudioLevelChanged.AddListener(onMicAudioLevelChanged.Invoke);
                events.OnMicStartedListening.AddListener(onMicStartedListening.Invoke);
                events.OnMicStoppedListening.AddListener(onMicStoppedListening.Invoke);
            }
        }

        private void OnDisable()
        {
            var events = AudioInputEvents;
            if (null != events)
            {
                events.OnMicAudioLevelChanged.RemoveListener(onMicAudioLevelChanged.Invoke);
                events.OnMicStartedListening.RemoveListener(onMicStartedListening.Invoke);
                events.OnMicStoppedListening.RemoveListener(onMicStoppedListening.Invoke);
            }
        }
    }
}
