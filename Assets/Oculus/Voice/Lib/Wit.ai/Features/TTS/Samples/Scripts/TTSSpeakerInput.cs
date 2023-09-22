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

using UnityEngine;
using UnityEngine.UI;
using Facebook.WitAi.TTS.Utilities;

namespace Facebook.WitAi.TTS.Samples
{
    public class TTSSpeakerInput : MonoBehaviour
    {
        [SerializeField] private Text _title;
        [SerializeField] private InputField _input;
        [SerializeField] private TTSSpeaker _speaker;

        // Preset text fields
        private void Update()
        {
            if (!string.Equals(_title.text, _speaker.presetVoiceID))
            {
                _title.text = _speaker.presetVoiceID;
                _input.placeholder.GetComponent<Text>().text = $"Write something to say in {_speaker.presetVoiceID}'s voice";
            }
        }

        // Either say the current phrase or stop talking/loading
        public void SayPhrase()
        {
            if (_speaker.IsLoading || _speaker.IsSpeaking)
            {
                _speaker.Stop();
            }
            else
            {
                _speaker.Speak(_input.text);
            }
        }
    }
}
