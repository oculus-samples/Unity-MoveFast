// Copyright (c) Meta Platforms, Inc. and affiliates.

using TMPro;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Handles the ui for the score/combo card
    /// </summary>
    public class ScoreCardUI : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField]
        private TextMeshProUGUI _totalScoreText;
        [SerializeField]
        private TextMeshProUGUI _comboScoreText;
        [SerializeField]
        private TextMeshProUGUI _multiplierText;
        [SerializeField]
        private TextMeshProUGUI _multiplierWordText;

        [Header("References")]
        [SerializeField]
        private ScoreKeeper _scoreKeeper;

        private void Awake()
        {
            _scoreKeeper.WhenChanged += UpdateScoreCardUI;
            UpdateScoreCardUI();
        }

        private void UpdateScoreCardUI()
        {
            _totalScoreText.SetText(_scoreKeeper.Score.ToString());
            _comboScoreText.SetText(_scoreKeeper.HitsInARow.ToString());
            _multiplierText.SetText($"x{_scoreKeeper.Combo}");
            UpdateMultiplierWord();
        }

        private void UpdateMultiplierWord()
        {
            if (_scoreKeeper.Combo < 10)
                _multiplierWordText.SetText("");
            else if (_scoreKeeper.Combo >= 10)
                _multiplierWordText.SetText("Awesome!");
            else if (_scoreKeeper.Combo >= 15)
                _multiplierWordText.SetText("Keep Going");
            else if (_scoreKeeper.Combo >= 20)
                _multiplierWordText.SetText("Amazing!");
        }
    }
}
