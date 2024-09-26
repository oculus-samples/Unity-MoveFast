// Copyright (c) Meta Platforms, Inc. and affiliates.

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
            return _nextAndPreviousLoops ? (int)Mathf.Repeat(index, _options.Count) : Mathf.Clamp(index, 0, _options.Count - 1);
        }

        private void SetCurrent(int index)
        {
            _stringProperty.Value = _options[index];
        }
    }
}
