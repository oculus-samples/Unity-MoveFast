// Copyright (c) Meta Platforms, Inc. and affiliates.

using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    public class IsAudioClipState : MonoBehaviour, IActiveState
    {
        public List<AudioClip> Clips;

        public bool Active => Clips.TrueForAll(x => x.loadState == AudioDataLoadState.Loaded);
    }
}
