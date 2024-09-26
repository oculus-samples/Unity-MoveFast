// Copyright (c) Meta Platforms, Inc. and affiliates.

using TMPro;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Populates the Results panel when a song ends
    /// </summary>
    public class ResultsUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private TextMeshProUGUI _highScoreText;
        [SerializeField]
        private TextMeshProUGUI _scoreText;
        [SerializeField]
        private TextMeshProUGUI _topMultiplierText;
        [SerializeField]
        private TextMeshProUGUI _topComboText;
        [SerializeField]
        private TextMeshProUGUI _topSpeedText;
        [SerializeField]
        private TextMeshProUGUI _hitCounter;
        [SerializeField]
        private GameObject _newHighScoreImage;

        [Header("References")]
        [SerializeField]
        private ExerciseResults _exerciseResults;

        private void Start()
        {
            _exerciseResults.WhenUpdated += UpdateUI;
        }

        private void UpdateUI()
        {
            _scoreText.SetText(_exerciseResults.Score.ToString());
            _topMultiplierText.SetText($"x{_exerciseResults.TopMultiplier}");
            _topComboText.SetText(_exerciseResults.TopCombo.ToString());
            _topSpeedText.SetText(_exerciseResults.TopSpeed.ToString("N1"));
            _hitCounter.SetText(_exerciseResults.SuccessfulHits.ToString() + "/" + _exerciseResults.TotalHits.ToString());
            var previousScore = _exerciseResults.PreviousHighScore;
            _highScoreText.SetText(previousScore > 0 ? previousScore.ToString() : "-");

            _newHighScoreImage.SetActive(_exerciseResults.Score > previousScore);
        }
    }
}
