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
using UnityEngine.Assertions;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Limits a StringProperty to a set of options and provides 
    /// Previous/Next functionality to switch between them
    /// </summary>
    public class StringPropertyOptions : MonoBehaviour
    {
        [SerializeField]
        private StringPropertyRef _stringProperty;
        [SerializeField]
        private List<string> _options = new List<string>();
        [SerializeField, Tooltip("When true, calling Next or Previous at the bounds will loop to the first or last option respectively")]
        private bool _nextAndPreviousLoops;

        public int CurrentIndex => _options.IndexOf(_stringProperty.Value);

        private void Reset()
        {
            _stringProperty.Property = GetComponent<IProperty<string>>();
        }

        private void Start()
        {
            _stringProperty.AssertNotNull();
            _stringProperty.WhenChanged += AssetValueIsOption;
        }

        private void OnDestroy()
        {
            _stringProperty.WhenChanged -= AssetValueIsOption;
        }

        private void AssetValueIsOption()
        {
            Assert.IsTrue(_options.Contains(_stringProperty.Value), $"{_stringProperty.Value} is not an option");
        }

        public void Next()
        {
            SetCurrent(ClampIndex(CurrentIndex + 1));
        }

        public void Previous()
        {
            SetCurrent(ClampIndex(CurrentIndex - 1));
        }

        private int ClampIndex(int index)
        {
            return _nextAndPreviousLoops ? (int)Mathf.Repeat(index, _options.Count) : Mathf.Clamp(index, 0, _options.Count-1);
        }

        private void SetCurrent(int index)
        {
            _stringProperty.Value = _options[index];
        }
    }
}
