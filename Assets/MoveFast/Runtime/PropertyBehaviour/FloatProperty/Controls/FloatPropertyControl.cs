// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Maps a FloatProperty to some other value
    /// </summary>
    public abstract class FloatPropertyControl : MonoBehaviour
    {
        [SerializeField]
        FloatPropertyRef _floatProperty;

        protected virtual void Awake()
        {
            _floatProperty.WhenChanged += UpdateControl;
            UpdateControl();
        }

        protected virtual void OnDestroy()
        {
            _floatProperty.WhenChanged -= UpdateControl;
        }

        public void UpdateControl() => UpdateControl(_floatProperty.Value);

        protected abstract void UpdateControl(float value);

        protected void UpdateProperty(float value)
        {
            _floatProperty.WhenChanged -= UpdateControl;
            _floatProperty.Value = value;
            _floatProperty.WhenChanged += UpdateControl;
        }
    }
}
