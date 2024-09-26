// Copyright (c) Meta Platforms, Inc. and affiliates.

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
