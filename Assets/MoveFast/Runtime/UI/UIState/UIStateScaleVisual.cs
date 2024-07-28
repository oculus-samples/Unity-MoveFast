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
    /// Controls localScale based on UIState
    /// </summary>
    public class UIStateScaleVisual : UIStateVisual
    {
        public UIScaleSet _normal = new UIScaleSet(Vector3.one, Vector3.one, new Vector3(0.92f, 0.92f, 0.92f));
        public UIScaleSet _active = new UIScaleSet(Vector3.one, Vector3.one, new Vector3(0.92f, 0.92f, 0.92f));
        [SerializeField]
        private bool _useActive = true;
        [SerializeField]
        private float _duration = 0.1f;

        protected override void UpdateVisual(IUIState uiState, bool animate)
        {
#if UNITY_EDITOR
            if (transform == null) { return; }
#endif
            var scale = (_useActive && uiState.Active ? _active : _normal).GetScaleForState(uiState.State);
            TweenRunner.Tween(transform.localScale, scale, _duration, x => transform.localScale = x)
                .SetID(this)
                .Skip(!animate);
        }
    }

    [System.Serializable]
    public struct UIScaleSet
    {
        [SerializeField]
        private Vector3 _normalScale;
        [SerializeField]
        private Vector3 _hoverScale;
        [SerializeField]
        private Vector3 _pressScale;

        public UIScaleSet(Vector3 normal, Vector3 hover, Vector3 press)
        {
            _normalScale = normal;
            _hoverScale = hover;
            _pressScale = press;
        }

        public Vector3 GetScaleForState(UIStates state)
        {
            switch (state)
            {
                case UIStates.Hovered: return _hoverScale;
                case UIStates.Pressed: return _pressScale;
                default: return _normalScale;
            }
        }
    }
}
