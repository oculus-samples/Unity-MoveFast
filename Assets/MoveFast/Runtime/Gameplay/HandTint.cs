// Copyright (c) Meta Platforms, Inc. and affiliates.

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
