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

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// A wrapper for text mesh pro that adds formatting
    /// </summary>
    public class StringLabel : MonoBehaviour
    {
        [SerializeField, TextArea]
        string[] _values = new string[1];

        [SerializeField, TextArea]
        string _format;

        private void OnValidate()
        {
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            TMPro.TextMeshProUGUI text = GetComponent<TMPro.TextMeshProUGUI>();
            if (string.IsNullOrWhiteSpace(_format))
            {
                text.text = string.Join(" ", _values);
            }
            else
            {
                text.text = string.Format(_format, _values);
            }
        }

        public void SetText(string value, int index = 0)
        {
            if (index >= 0 || index < _values.Length)
            {
                _values[index] = value;
                UpdateLabel();
            }
        }
    }
}
