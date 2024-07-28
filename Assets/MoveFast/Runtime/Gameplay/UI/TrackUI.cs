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
