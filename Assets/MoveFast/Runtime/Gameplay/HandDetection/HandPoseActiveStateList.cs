// Copyright (c) Meta Platforms, Inc. and affiliates.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// A list of named IActiveStates
    /// </summary>
    public class HandPoseActiveStateList : MonoBehaviour
    {
        [SerializeField, Interface(typeof(IActiveState))]
        List<MonoBehaviour> _poses;

        internal IActiveState Get(string poseName)
        {
            return _poses.Find(x => x.name == poseName) as IActiveState;
        }

        public int ActiveCount()
        {
            int count = 0;
            foreach (var pose in _poses)
            {
                if ((pose as IActiveState).Active) count++;
            }
            return count;
        }

        private void OnEnable()
        {
            StartCoroutine(routine());
            IEnumerator routine()
            {
                yield return null;
                _activeStates.Clear();
                _poses.ForEach(x => _activeStates.Add(x.name, new DelayedFalseActiveState(x as IActiveState, 0.05f)));
                while (true)
                {
                    foreach (var x in _activeStates) x.Value.Update();
                    yield return null;
                }
            }
        }

        private Dictionary<string, DelayedFalseActiveState> _activeStates = new Dictionary<string, DelayedFalseActiveState>();

        class DelayedFalseActiveState : IActiveState
        {
            private float _delay = 0.1f;
            private IActiveState _activeState;
            private float _lastActiveTime;

            public DelayedFalseActiveState(IActiveState activeState, float delay)
            {
                _activeState = activeState;
                _lastActiveTime = -1;
                _delay = delay;
                Update();
            }

            public bool Active
            {
                get
                {
                    Update();
                    return Time.time - _lastActiveTime < _delay;
                }
            }

            internal void Update()
            {
                if (_activeState.Active) _lastActiveTime = Time.time;
            }
        }
    }
}
