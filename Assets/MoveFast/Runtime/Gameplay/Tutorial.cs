// Copyright (c) Meta Platforms, Inc. and affiliates.

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
