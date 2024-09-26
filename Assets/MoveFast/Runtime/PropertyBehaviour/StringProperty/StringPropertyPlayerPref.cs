// Copyright (c) Meta Platforms, Inc. and affiliates.

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
