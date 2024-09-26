// Copyright (c) Meta Platforms, Inc. and affiliates.

using Oculus.Interaction.MoveFast;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Modifies the `enabled` state of a list of components, from the `Active` field of the given IActiveState.
    /// Similar to ActiveStateTracker except supports inverting the IActiveState and supports Behaviour types
    /// rather than MonoBehaviour (e.g. AudioSource)
    /// </summary>
    public class ConditionalComponentsEnabled : ActiveStateObserver
    {
        [SerializeField]
        [Tooltip("Sets the `enabled` field on individual components")]
        List<Behaviour> _behaviours = new List<Behaviour>();

        protected override void Start()
        {
            base.Start();
            HandleActiveStateChanged();
        }

        protected override void HandleActiveStateChanged() => _behaviours.ForEach(x => x.enabled = Active);
    }
}
