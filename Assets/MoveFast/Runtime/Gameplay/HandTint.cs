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
    /// Changes the color of the hand based on an IActiveState
    /// </summary>
    public class HandTint : MonoBehaviour
    {
        [SerializeField]
        private string _propertyName;
        [SerializeField]
        private List<MaterialPropertyBlockEditor> _materialPropertyBlockEditors;
        [SerializeField]
        private ReferenceActiveState _isPlaying;
        [SerializeField]
        private List<ActiveColor> _activeColors = new List<ActiveColor>();

        private Color _defaultColor;

        private void Awake()
        {
            _defaultColor = _materialPropertyBlockEditors[0].Renderers[0].sharedMaterial.GetColor(_propertyName);
        }

        private void Update() => UpdateAcitveColor();

        public void UpdateAcitveColor()
        {
            var index = _isPlaying ? _activeColors.FindIndex(x => x.Active) : -1;
            var color = index != -1 ? _activeColors[index].Color : _defaultColor;

            for (int i = 0; i < _materialPropertyBlockEditors.Count; i++)
                _materialPropertyBlockEditors[i].MaterialPropertyBlock.SetColor(_propertyName, color);
        }
    }

    [System.Serializable]
    public struct ActiveColor
    {
        public ReferenceActiveState Active;
        public Color Color;
    }
}
