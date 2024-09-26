// Copyright (c) Meta Platforms, Inc. and affiliates.

using TMPro;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Sets the spawned object to display it's score
    /// </summary>
    public class SpawnerScore : MonoBehaviour, ISpawnerModifier
    {
        private ScoreIncrementer _hitDetector;

        void Awake()
        {
            _hitDetector = GetComponentInParent<ScoreIncrementer>();
        }

        public void Modify(GameObject instance)
        {
            instance.GetComponentInChildren<TextMeshProUGUI>().text = _hitDetector.LastScore.ToString();
        }
    }
}
