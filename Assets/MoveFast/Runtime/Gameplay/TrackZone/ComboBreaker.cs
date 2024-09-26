// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Breaks the combo score when the player misses a target
    /// When this object has been in the TrackZone and leaves and
    /// the _activeGo is still active it's assumed that the target was missed
    /// </summary>
    public class ComboBreaker : MonoBehaviour, IActiveState
    {
        [SerializeField, Tooltip("Is this GameObject is active when it leaves the TrackZone then it will break the combo")]
        private GameObject _activeGo;

        private TriggerZoneList<TrackZone> _trackZone;
        private ScoreKeeper _score;
        [SerializeField]
        Spawner _missSpawner;
        [SerializeField]
        AudioSource _audioSource;

        public bool Active => _trackZone.Count > 0;

        void Start()
        {
            _score = FindObjectOfType<ScoreKeeper>();

            // when the target is hit turn off the gameobject
            var hit = GetComponent<HandHitDetector>();
            hit.onHit.AddListener(() => _activeGo.SetActive(false));

            // when the target leaves the area test for combo break
            _trackZone = new TriggerZoneList<TrackZone>(gameObject.AddComponent<TriggerZone>());
            _trackZone.WhenAdded += EnableGameObject;
            _trackZone.WhenRemoved += BreakCombo;
        }

        private void EnableGameObject(TrackZone obj)
        {
            _activeGo.SetActive(true);
            _score.AddToTotalHits();
        }

        void OnDestroy()
        {
            _trackZone.Dispose();
            _trackZone = null;
        }

        private void BreakCombo(TrackZone _)
        {
            if (_activeGo.activeSelf)
            {
                _score.BreakCombo();
                _missSpawner.Spawn();
                _audioSource.Play();

            }
        }
    }
}
