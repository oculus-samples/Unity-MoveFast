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

using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Synchronizes a button or toggle with a StringProperty
    /// </summary>
    public class SetPropertyToggleButton : MonoBehaviour
    {
        [SerializeField]
        private StringPropertyRef _property;

        [Tooltip("When the property has this value the toggle will be ticked, tick on the toggle/button will set the property to this value")]
        public string OnValue;

        [Tooltip("When the userIteration is OnOff and the user unticks the toggle, the property will be set to this value")]
        public string OffValue;

        [SerializeField, Tooltip("For radio button like functionality use OnlyOn, otherwise OnOff will allow the user to disable this toggle")]
        UserInteraction _userInteraction = UserInteraction.OnlyOn;

        Selectable _selectable;
        ToggleGroup _toggleGroup;

        private void Awake()
        {
            _selectable = TryGetComponent<Button>(out var button) ? button : (Selectable)GetComponent<Toggle>();
        }

        private void Start()
        {
            _property.AssertNotNull();
            Assert.IsNotNull(_selectable);

            switch (_selectable)
            {
                case Button button:
                    button.onClick.AddListener(SetProperty);
                    break;
                case Toggle toggle:
                    toggle.group = _toggleGroup = gameObject.AddComponent<ToggleGroup>();
                    _property.WhenChanged += UpdateToggle;
                    UpdateToggle();
                    break;
                default:
                    throw new Exception();
            }
        }

        private void UpdateToggle()
        {
            var toggle = _selectable as Toggle;
            bool shouldBeOn = _property.Value == OnValue;

            _toggleGroup.allowSwitchOff = true;

            toggle.onValueChanged.RemoveListener(SetProperty);
            toggle.isOn = shouldBeOn;
            toggle.onValueChanged.AddListener(SetProperty);

            _toggleGroup.allowSwitchOff = !shouldBeOn || _userInteraction == UserInteraction.OnOff;
        }

        private void SetProperty() => SetProperty(true);
        private void SetProperty(bool _)
        {
            bool isOn = _selectable is Toggle toggle ? toggle.isOn : true;
            StringPropertyBehaviourRef.SetPropertyWithString(_property, isOn ? OnValue : OffValue);
        }

        enum UserInteraction
        {
            OnOff,
            OnlyOn
        }
    }
}
