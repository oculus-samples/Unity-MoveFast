// Copyright (c) Meta Platforms, Inc. and affiliates.

using System;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Tracks the consequtive hits and thier velocity to calculate the score
    /// </summary>
    public class ScoreKeeper : MonoBehaviour
    {
        [SerializeField, Tooltip("The number of consecutive hits require to up the score multipier")]
        private int _comboIncrement = 4;
        [SerializeField]
        private int _scoreGenericMultiplier = 10;

        private int _score = 0;
        private int _combo = 0;
        private int _hitsInARow = 0;
        private int _hitsTotal = 0;
        private int _hitsSuccessful = 0;

        public event Action WhenChanged;

        public int Score => _score;
        public int HitsTotal => _hitsTotal;
        public int HitsSuccessful => _hitsSuccessful;
        public int Combo => (_combo / _comboIncrement) + 1;
        public int HitsInARow => _hitsInARow;

        public float TopSpeed { get; private set; }
        public int TopHitsInARow { get; private set; }
        public int TopMultiplier { get; private set; }

        public void AddSpeed(float speed)
        {
            TopSpeed = Mathf.Max(TopSpeed, speed);
        }

        public int AddScore(int bonus)
        {
            int comboScore = ((_combo + (bonus * _scoreGenericMultiplier)) / _comboIncrement) + 1;
            comboScore *= _scoreGenericMultiplier;
            _score += comboScore;
            _combo += bonus + 1;
            _hitsInARow++;
            _hitsSuccessful++;
            TopHitsInARow = Math.Max(TopHitsInARow, _hitsInARow);
            TopMultiplier = Math.Max(TopMultiplier, Combo);

            WhenChanged();
            return comboScore;
        }

        public int AddScore()
        {
            int comboScore = (_combo / _comboIncrement) + 1;
            comboScore *= _scoreGenericMultiplier;
            _score += comboScore;
            _combo++;
            _hitsInARow++;
            _hitsSuccessful++;

            TopHitsInARow = Math.Max(TopHitsInARow, _hitsInARow);
            TopMultiplier = Math.Max(TopMultiplier, Combo);

            WhenChanged();
            return comboScore;
        }
        public void AddToTotalHits()
        {
            _hitsTotal++;
        }
        public void BreakCombo()
        {
            _combo = _combo - (_combo % _comboIncrement) - 1;
            _hitsInARow = 0;
            WhenChanged();
        }

        public void ResetScore()
        {
            _score = 0;
            _combo = 0;
            _hitsInARow = 0;
            _hitsSuccessful = 0;
            _hitsTotal = 0;
            TopSpeed = 0;
            TopMultiplier = 0;
            TopHitsInARow = 0;
            WhenChanged();
        }
    }
}
