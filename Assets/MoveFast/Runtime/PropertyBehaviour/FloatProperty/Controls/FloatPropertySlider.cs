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
