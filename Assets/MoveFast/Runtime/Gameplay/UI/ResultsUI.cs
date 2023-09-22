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
