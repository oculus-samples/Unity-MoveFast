// Copyright (c) Meta Platforms, Inc. and affiliates.

using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Updates the material on a particle system
    /// We do this instead of setting particle color because this will immediatly update all particles
    /// where setting the color only update newly emitted particles
    /// </summary>
    public class ParticleMaterialSelector : MonoBehaviour
    {
        [SerializeField]
        private List<MaterialPair> _materialPairs;

        private ParticleSystemRenderer _renderer;

        void Start()
        {
            _renderer = GetComponent<ParticleSystemRenderer>();
        }

        private void Update()
        {
            for (int i = 0; i < _materialPairs.Count - 1; i++)
            {
                if (_materialPairs[i].activeState.Active)
                {
                    _renderer.sharedMaterial = _materialPairs[i].material;
                    return;
                }
            }
            _renderer.sharedMaterial = _materialPairs[_materialPairs.Count - 1].material;
        }

        [System.Serializable]
        struct MaterialPair
        {
            public Material material;
            public ReferenceActiveState activeState;
        }
    }
}
