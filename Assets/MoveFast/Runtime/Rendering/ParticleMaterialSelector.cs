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
