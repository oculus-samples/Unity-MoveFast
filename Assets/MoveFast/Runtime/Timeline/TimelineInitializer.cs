// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;
using UnityEngine.Playables;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Evaluates a timeline in Start
    /// </summary>
    public class TimelineInitializer : MonoBehaviour
    {
        void Start() => GetComponent<PlayableDirector>().Evaluate();
    }
}
