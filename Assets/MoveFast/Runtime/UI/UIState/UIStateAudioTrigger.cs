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

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Emits audio in response to UI hover events
    /// </summary>
    public class UIStateAudioTrigger : UIStateVisual
    {
        [SerializeField, Optional]
        private AudioTrigger _normal, _hover, _press;

        private UIStates _state = UIStates.None;

        protected override void UpdateVisual(IUIState uiState, bool animate)
        {
            if (!Application.isPlaying) return;
            if (!animate) return; //assume if not animate then it was probably enable/disable and we dont want sound
            if (_state == uiState.State) return;

            if (TryGetAudio(_state, out var previous)) previous.StopAudio();
            if (TryGetAudio(uiState.State, out var next)) next.PlayAudio();

            _state = uiState.State;
        }

        private bool TryGetAudio(UIStates state, out AudioTrigger audio)
        {
            return (audio = GetAudio(state)) && audio != null;
        }

        AudioTrigger GetAudio(UIStates state)
        {
            switch (state)
            {
                case UIStates.Normal: return _normal;
                case UIStates.Hovered: return _hover;
                case UIStates.Pressed: return _press;
                default: return null;
            }
        }
    }
}
