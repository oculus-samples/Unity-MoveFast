// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;
using UnityEngine.UI;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Maps a FloatProperty onto a UI Slider
    /// </summary>
    [RequireComponent(typeof(Slider))]
    public class FloatPropertySlider : FloatPropertyControl
    {
        [SerializeField]
        Property _property;

        private Slider _slider;

        protected override void Awake()
        {
            _slider = GetComponent<Slider>();
            if (_property == Property.Value)
            {
                _slider.onValueChanged.AddListener(UpdateProperty);
            }
            base.Awake();
            UpdateControl();
        }

        protected override void UpdateControl(float value)
        {
            switch (_property)
            {
                case Property.Value:
                    _slider.onValueChanged.RemoveListener(UpdateProperty);
                    _slider.value = value;
                    _slider.onValueChanged.AddListener(UpdateProperty);
                    return;
                case Property.Min:
                    _slider.minValue = value;
                    return;
                case Property.Max:
                    _slider.maxValue = value;
                    return;
                default: throw new System.Exception("FloatPropertySlider property invalid");
            }
        }

        enum Property
        {
            Value,
            Min,
            Max
        }
    }
}
