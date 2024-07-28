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
    /// Handles the UI functionality for the combo on the track
    /// </summary>
    public class ComboUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _comboScoreText;
        [SerializeField]
        private TextMeshProUGUI _comboWordText;
        [SerializeField]
        private ScoreKeeper _scoreKeeper;
        [SerializeField]
        private UICanvas _comboCanvas;

        private void Awake()
        {
            _scoreKeeper.WhenChanged += UpdateScoreCardUI;
            UpdateScoreCardUI();
        }

        private void Update()
        {
            var currentTrack = TrackTimeline.Current?.PlayableDirector;
            bool shouldShow = currentTrack != null;
            if (shouldShow)
            {
                shouldShow &= currentTrack.name != "AirboxingTutorial"; //TODO not name reliant
                shouldShow &= currentTrack.time > 3 && currentTrack.time < currentTrack.duration - 2;
            }
            _comboCanvas.Show(shouldShow);
        }

        private void UpdateScoreCardUI()
        {
            string hits = _scoreKeeper.HitsInARow.ToString();
            if (_comboScoreText.text != hits) _comboScoreText.text = hits;

            var text = TextForScore(_scoreKeeper.HitsInARow);
            if (_comboWordText.text != text) _comboWordText.text = text;
        }

        string TextForScore(int hitsInARow)
        {
            if (hitsInARow >= 20) return "Amazing!";
            if (hitsInARow >= 15) return "Keep Going";
            if (hitsInARow >= 10) return "Awesome!";
            if (hitsInARow >= 5) return "Great Job";
            return "";
        }
    }
}
