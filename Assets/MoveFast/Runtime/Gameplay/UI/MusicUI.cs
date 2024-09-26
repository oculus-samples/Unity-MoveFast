// Copyright (c) Meta Platforms, Inc. and affiliates.

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Updates UI with data from a MusicPreset object
    /// </summary>
    public class MusicUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private Image _trackCoverArt;
        [SerializeField]
        private TextMeshProUGUI _trackName;
        [SerializeField]
        private TextMeshProUGUI _trackArtist;
        [SerializeField]
        private TextMeshProUGUI _trackDetails;
        [SerializeField]
        private Image _trackDifficulty;

        private MusicPreset _musicPreset;
        public MusicPreset MusicPreset
        {
            get => _musicPreset;
            set
            {
                _musicPreset = value;
                UpdateMusicInformation();
            }
        }

        private void UpdateMusicInformation()
        {
            _trackCoverArt.sprite = _musicPreset.TrackCoverArt;
            _trackName.SetText(_musicPreset.TrackName);
            _trackArtist.SetText("By " + _musicPreset.TrackArtist);
            _trackDetails.SetText(_musicPreset.TrackGenre + " | " + _musicPreset.TrackBPM + " bpm");
            _trackDifficulty.sprite = _musicPreset.TrackDifficulty;
        }
    }
}
