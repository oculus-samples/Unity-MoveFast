// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Destroys an object after a delay
    /// </summary>
    public class DestroyAfterDelay : MonoBehaviour
    {
        [SerializeField]
        float _delay = 2;

        private void Start() => Destroy(gameObject, _delay);
    }
}
