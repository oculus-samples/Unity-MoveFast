// Copyright (c) Meta Platforms, Inc. and affiliates.

using Oculus.Interaction;
using Oculus.Interaction.MoveFast;
using System.Collections;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Plays an AudioTrigger based on an IActiveState
    /// </summary>
    public class ActiveStateAudioTrigger : ActiveStateObserver
    {
        [SerializeField]
        AudioTrigger _audioTrigger;
        [SerializeField]
        float _fadeIn = 0.3f;
        [SerializeField]
        float _fadeOut = 0.1f;

        protected override void HandleActiveStateChanged()
        {
            if (Active)
            {
                _audioTrigger.PlayAudio(_fadeIn);
            }
            else
            {
                _audioTrigger.StopAudio(_fadeOut);
            }
        }
    }
}
