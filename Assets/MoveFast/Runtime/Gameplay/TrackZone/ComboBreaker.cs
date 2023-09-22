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
