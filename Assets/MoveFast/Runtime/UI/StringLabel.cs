// Copyright (c) Meta Platforms, Inc. and affiliates.

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
