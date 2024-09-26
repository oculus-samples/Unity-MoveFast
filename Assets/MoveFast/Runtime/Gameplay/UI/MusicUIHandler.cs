// Copyright (c) Meta Platforms, Inc. and affiliates.

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
