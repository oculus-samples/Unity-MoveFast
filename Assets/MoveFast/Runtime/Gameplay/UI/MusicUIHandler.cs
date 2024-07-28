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

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Updates multiple music UI when the current song changes
    /// </summary>
    public class MusicUIHandler : MonoBehaviour
    {
        [SerializeField]
        private MusicPreset _musicPreset;
        [SerializeField]
        private MusicUI _musicInfoPanel, _resultsPanel,
            _currentTrackPanel, _playerCurrentTrackUI;
        [SerializeField]
        private TrackUI _currentTrackUI;

        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(UpdateInformation);
        }

        private void UpdateInformation()
        {
            _currentTrackUI.TrackID = GetComponent<SetPropertyToggleButton>().OnValue;
            _musicInfoPanel.MusicPreset = _musicPreset;
            _resultsPanel.MusicPreset = _musicPreset;
            _currentTrackPanel.MusicPreset = _musicPreset;
            _playerCurrentTrackUI.MusicPreset = _musicPreset;
        }
    }
}
