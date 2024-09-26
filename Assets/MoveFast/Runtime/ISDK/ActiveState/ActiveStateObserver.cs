// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Checks for an ActiveState to have changed in Update
    /// Similar to ActiveStateSelector but using ReferenceActiveState and designed for inheritance
    /// </summary>
    public abstract class ActiveStateObserver : MonoBehaviour
    {
        [SerializeField]
        private ReferenceActiveState _activeState;

        protected bool Active { get; private set; }

        protected virtual void Reset()
        {
            _activeState.InjectActiveState(GetComponent<IActiveState>());
        }

        protected virtual void Start()
        {
            _activeState.AssertNotNull($"{name} ({GetType()}) requires an IActiveState assigned");
        }

        protected virtual void Update()
        {
            if (Active != _activeState.Active)
            {
                Active = !Active;
                HandleActiveStateChanged();
            }
        }

        protected abstract void HandleActiveStateChanged();

        #region Inject
        public void InjectActiveState(IActiveState activeState) => _activeState.InjectActiveState(activeState);
        #endregion
    }
}
