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
    /// Makes a string property persistent
    /// </summary>
    public class StringPropertyPlayerPref : MonoBehaviour
    {
        [SerializeField]
        private StringPropertyRef _stringProperty;
        [SerializeField]
        private string _playerPrefKey;
        [SerializeField]
        private bool _clearInEditor = true;

        private void Reset()
        {
            _stringProperty.Property = GetComponent<IProperty<string>>();
        }

        private void Start()
        {
            if (_clearInEditor) { Store.DeleteKey(_playerPrefKey); }

            _stringProperty.AssertNotNull();

            if (PlayerPrefs.HasKey(_playerPrefKey))
            {
                _stringProperty.Value = Store.GetString(_playerPrefKey);
            }

            _stringProperty.WhenChanged += UpdatePlayerPref;
            Store.WhenChanged += UpdateStringProperty;
        }

        private void OnDestroy()
        {
            _stringProperty.WhenChanged -= UpdatePlayerPref;
            Store.WhenChanged -= UpdateStringProperty;
        }

        private void UpdateStringProperty()
        {
            _stringProperty.WhenChanged -= UpdatePlayerPref;
            _stringProperty.Value = Store.GetString(_playerPrefKey);
            _stringProperty.WhenChanged += UpdatePlayerPref;
        }

        private void UpdatePlayerPref()
        {
            Store.WhenChanged -= UpdateStringProperty;
            Store.SetString(_playerPrefKey, _stringProperty.Value);
            Store.WhenChanged += UpdateStringProperty;
        }
    }
}
