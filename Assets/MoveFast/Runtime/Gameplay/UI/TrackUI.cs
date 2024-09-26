// Copyright (c) Meta Platforms, Inc. and affiliates.

using TMPro;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Displays saved player data about the track
    /// </summary>
    public class TrackUI : MonoBehaviour
    {
        [Header("ScoreCard Components")]
        [SerializeField]
        private TextMeshProUGUI _topScoreText;
        [SerializeField]
        private TextMeshProUGUI _topComboText;
        [SerializeField]
        private TextMeshProUGUI _topSpeedText;
        [SerializeField]
        private TextMeshProUGUI _attemptsText;

        public string TrackID
        {
            get => _trackID;
            set
            {
                _trackID = value;
                UpdateSongHighScore();
            }
        }

        private string _trackID;

        private void UpdateSongHighScore()
        {
            string songSelection = _trackID;
            _topScoreText.SetText(Store.GetInt($"score-{songSelection}").ToString());
            _topComboText.SetText(Store.GetInt($"combo-{songSelection}").ToString());
            _topSpeedText.SetText(Store.GetFloat($"speed-{songSelection}").ToString("N1"));
            _attemptsText.SetText(Store.GetInt($"attempts-{songSelection}").ToString());
        }
    }
}
