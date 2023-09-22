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

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Controls the tutorial's behaviour
    /// </summary>
    public class Tutorial : MonoBehaviour
    {
        private PlayableDirector _director;
        private List<TimeRange> _timeRanges;
        private ScoreKeeper _scoreKeeper = null;
        private int _index = -1;

        [SerializeField]
        ReferenceActiveState _isBlocking;
        [SerializeField]
        ReferenceActiveState _isTutorial;
        [SerializeField]
        int _blockIndex = 5;

        float _lastNext = -1;

        private void Start()
        {
            _scoreKeeper = FindObjectOfType<ScoreKeeper>();
            _director = GetComponent<PlayableDirector>();
            _timeRanges = MarkerTrack.GetTimeRanges(_director);

            _director.RebuildGraph();

            _director.played += StartTutorial;
            _director.stopped += EndTutorial;
        }


        private void StartTutorial(PlayableDirector obj)
        {
            _index = -1;

            HandHitDetector.TutorialMode = true;
            _scoreKeeper.WhenChanged -= Next;
            _scoreKeeper.WhenChanged += Next;
            Next(true);
        }

        private void EndTutorial(PlayableDirector obj)
        {
            HandHitDetector.TutorialMode = false;
            _scoreKeeper.WhenChanged -= Next;
            _index = -1;
        }

        private void Update()
        {
            if (_isTutorial)
            {
                HandHitDetector.TutorialMode = true;
            }
            else
            {
                HandHitDetector.TutorialMode = false;
            }
            if (DetectBlock())
            {
                Next();
            }
        }

        private bool DetectBlock()
        {
            // dont bother checking for blocks on the punching steps
            if (_index < _blockIndex) return false;
            // dont bother if the directors not playing
            if (!_director.playableGraph.IsValid()) return false;
            // dont bother if the director isnt at a stop/pause point
            bool directorIsAtEnd = _director.time == _director.playableGraph.GetRootPlayable(0).GetDuration();
            if (!directorIsAtEnd) return false;

            return _isBlocking;
        }

        public void Next()
        {
            Next(false);
        }

        public void Next(bool forceIndex)
        {
            Debug.Log("Next");
            if (Time.time - _lastNext < 1 && !forceIndex) return; // HACK dont allow the user to progress too fast
            _lastNext = Time.time;

            _index++;

            var end = _index >= _timeRanges.Count;
            var time = end ? _director.playableAsset.duration : _timeRanges[_index].End;
            _director.playableGraph.GetRootPlayable(0).SetDuration(time);
            Debug.Log($"Next 2 {_index} {time}");

            if (end)
            {
                Debug.Log("Next 2");
                EndTutorial(null);
                _index = -1;
            }
        }
    }
}
