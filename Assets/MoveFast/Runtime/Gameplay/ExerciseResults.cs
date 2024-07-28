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

using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// When the round ends, saves the data of a round to display in the result panel
    /// </summary>
    public partial class ExerciseResults : MonoBehaviour
    {
        [SerializeField]
        ScoreKeeper _scoreKeeper;
        public ScoreKeeper ScoreKeeper => _scoreKeeper;
        [SerializeField, FormerlySerializedAs("_stringProperty")]
        StringPropertyRef _appState;
        [SerializeField]
        StringPropertyRef _songSelection;
        public IProperty<string> SongSelection => _songSelection;

        public event Action WhenUpdated;

        public int Score { get; private set; }
        public int TopMultiplier { get; private set; }
        public int TopCombo { get; private set; }
        public float TopSpeed { get; private set; }
        public float Attempts { get; private set; }
        public float PreviousHighScore { get; private set; }
        public int SuccessfulHits { get; private set; }
        public int TotalHits { get; private set; }

        private void Start()
        {
            _scoreKeeper = FindObjectOfType<ScoreKeeper>();
            TrackTimeline.WhenTrackEnded += EndGame;
        }

        private void EndGame(TrackTimeline obj)
        {
            PreviousHighScore = Store.GetInt($"score-{_songSelection.Value}");
            Score = _scoreKeeper.Score;
            TopSpeed = _scoreKeeper.TopSpeed;
            TopCombo = _scoreKeeper.TopHitsInARow;
            TopMultiplier = _scoreKeeper.TopMultiplier;
            SuccessfulHits = _scoreKeeper.HitsSuccessful;
            TotalHits = _scoreKeeper.HitsTotal;

            Store.SetMaxInt($"combo-{_songSelection.Value}", TopCombo);
            Store.SetMaxInt($"score-{_songSelection.Value}", Score);
            Store.SetMaxFloat($"speed-{_songSelection.Value}", TopSpeed);
            Attempts = Store.Increment($"attempts-{_songSelection.Value}");

            _scoreKeeper.ResetScore();
            _appState.Value = "results";
            WhenUpdated?.Invoke();
        }
    }
}
