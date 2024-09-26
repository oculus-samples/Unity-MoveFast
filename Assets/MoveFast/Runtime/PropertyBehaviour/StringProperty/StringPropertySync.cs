// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Updates one string property in response to another
    /// e.g. to switch to a default sub page when a page is opened
    /// </summary>
    public class StringPropertySync : ActiveStateObserver
    {
        [SerializeField]
        StringPropertyRef _property;

        [SerializeField]
        StateChange _becameActive;

        [SerializeField]
        StateChange _becameInactive;

        protected override void HandleActiveStateChanged()
        {
            if (Active && _becameActive.enabled)
            {
                _property.Value = _becameActive.value;
            }
            else if (!Active && _becameInactive.enabled)
            {
                _property.Value = _becameInactive.value;
            }
        }

        [System.Serializable]
        struct StateChange
        {
            public bool enabled;
            public string value;
        }
    }
}
