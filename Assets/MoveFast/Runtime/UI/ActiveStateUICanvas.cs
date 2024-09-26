// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Maps an ActiveState onto a UICanvas's Show value
    /// </summary>
    [RequireComponent(typeof(UICanvas)), DisallowMultipleComponent]
    public class ActiveStateUICanvas : ActiveStateObserver
    {
        private UICanvas _canvas;

        private void Awake()
        {
            _canvas = GetComponent<UICanvas>();
            HandleActiveStateChanged();
        }

        protected override void HandleActiveStateChanged()
        {
            _canvas.Show(Active);
        }
    }
}
