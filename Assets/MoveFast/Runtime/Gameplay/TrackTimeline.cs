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
using UnityEngine.Playables;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Controls the timelines that contain the exercises
    /// </summary>
    public class TrackTimeline : MonoBehaviour
    {
        public static event Action<TrackTimeline> WhenTrackEnded;

        [SerializeField]
        PlayableDirector _playableDirector;
        [SerializeField, Tooltip("Controls if the timeline should Play or Stop")]
        private ReferenceActiveState _active;
        [SerializeField, Tooltip("Controls if the timeline should Pause or Resume")]
        private ReferenceActiveState _paused;

        PlayState _lastPlayState = PlayState.None;

        public static TrackTimeline Current { get; private set; }
        public PlayableDirector PlayableDirector => _playableDirector;

        protected virtual void Reset()
        {
            _active.InjectActiveState(GetComponent<IActiveState>());
            _paused.InjectActiveState(GetComponent<IActiveState>());
        }

        private void Awake()
        {
            _playableDirector.extrapolationMode = DirectorWrapMode.Hold;
        }

        private void Start()
        {
            _playableDirector.RebuildGraph();
            _playableDirector.Evaluate();
        }

        protected virtual void Update()
        {
            var playState = _active ? _paused ?
                PlayState.Paused :
                PlayState.Playing :
                PlayState.Stopped;

            if (playState != _lastPlayState)
            {
                switch (playState)
                {
                    case PlayState.Stopped:
                        Stop();
                        break;
                    case PlayState.Paused:
                        _playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
                        break;
                    case PlayState.Playing:
                        PlaySong();
                        break;
                }

                _lastPlayState = playState;
            }

            if (_lastPlayState != PlayState.Stopped && _playableDirector.state == UnityEngine.Playables.PlayState.Playing && _playableDirector.time >= _playableDirector.playableAsset.duration)
            {
                Stop();
                WhenTrackEnded?.Invoke(this);
            }
        }

        private void Stop()
        {
            _lastPlayState = PlayState.Stopped;
            _playableDirector.Pause();
            _playableDirector.time = 0;
            _playableDirector.Evaluate();
            if (Current == this) Current = null;
        }

        private void PlaySong()
        {
            Current = this;
            if (_playableDirector.state != UnityEngine.Playables.PlayState.Playing)
            {
                _playableDirector.Play();
            }
            _playableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }

        private enum PlayState
        {
            None,
            Stopped,
            Paused,
            Playing
        }
    }
}
