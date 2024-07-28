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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Broadcasts UI hover states to children
    /// </summary>
    [ExecuteAlways]
    public class UIState : MonoBehaviour, IUIState,
        IPointerDownHandler, IPointerUpHandler,
        IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private UIStates _editorPreview = UIStates.None;

        public event Action WhenChanged;
        public bool Interactable => _groupsAllowInteraction;
        public bool Focused { get; private set; } = false;
        public bool Active
        {
            get
            {
                InitSelectable();
                return _isSelectable.Value && _selectable is Toggle t && t.isOn;
            }
        }

        public UIStates State
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying && _editorPreview != UIStates.None) return _editorPreview;
#endif
                InitSelectable();
                if (!_groupsAllowInteraction || (_isSelectable.Value && !_isSelectableInteractable)) return UIStates.Disabled;
                if (_isPointerDown) return UIStates.Pressed;
                if (_isPointerInside) return UIStates.Hovered;
                return UIStates.Normal;
            }
        }

        private readonly List<CanvasGroup> _canvasGroupCache = new List<CanvasGroup>();
        private bool _groupsAllowInteraction = true;
        private bool _isPointerInside;
        private bool _isPointerDown;
        private Selectable _selectable;
        private bool? _isSelectable;
        private bool _isSelectableInteractable;

        private void OnValidate()
        {
            InvokeWhenChanged();
        }

        void OnEnable()
        {
            _editorPreview = UIStates.None;
            InitSelectable();

            if (_isSelectable.Value && _selectable is Toggle toggle)
            {
                toggle.onValueChanged.AddListener(InvokeWhenChanged);
            }
        }

        private void InitSelectable()
        {
            if (_isSelectable.HasValue) return;
            _isSelectable = TryGetComponent(out _selectable);
            _isSelectableInteractable = _isSelectable.Value && _selectable.interactable;
        }

        void OnDisable()
        {
            if (_isSelectable.Value && _selectable is Toggle toggle)
            {
                toggle.onValueChanged.RemoveListener(InvokeWhenChanged);
            }
        }

        void LateUpdate()
        {
            if (_isSelectable.Value && _isSelectableInteractable != _selectable.interactable)
            {
                _isSelectableInteractable = _selectable.interactable;
                InvokeWhenChanged();
            }
        }

        private void InvokeWhenChanged(bool _) => InvokeWhenChanged();

        private void InvokeWhenChanged()
        {
            WhenChanged?.Invoke();
        }

        private void OnCanvasGroupChanged()
        {
            var groupAllowInteraction = true;
            Transform t = transform;
            while (t != null)
            {
                t.GetComponents(_canvasGroupCache);
                bool shouldBreak = false;
                for (var i = 0; i < _canvasGroupCache.Count; i++)
                {
                    if (!_canvasGroupCache[i].interactable)
                    {
                        groupAllowInteraction = false;
                        shouldBreak = true;
                    }

                    if (_canvasGroupCache[i].ignoreParentGroups)
                        shouldBreak = true;
                }
                if (shouldBreak)
                    break;

                t = t.parent;
            }

            if (groupAllowInteraction != _groupsAllowInteraction)
            {
                _groupsAllowInteraction = groupAllowInteraction;
                InvokeWhenChanged();
            }
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            _isPointerDown = true;
            InvokeWhenChanged();
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            _isPointerDown = false;
            InvokeWhenChanged();
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            _isPointerInside = true;
            InvokeWhenChanged();
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            _isPointerInside = false;
            InvokeWhenChanged();
        }
    }

    public interface IUIState
    {
        public bool Focused { get; }
        /// <summary>
        /// The UIState will be active if it's a toggle and the toggle is turned on
        /// </summary>
        public bool Active { get; }
        public UIStates State { get; }
    }

    public enum UIStates
    {
        Normal,
        Hovered,
        Pressed,
        Disabled,
        None
    }

    [Serializable]
    public struct UIStateValues<T>
    {
        public T normal;
        public T hovered;
        public T pressed;
        public T disabled;

        public T GetValue(UIStates state, T defaultValue)
        {
            switch (state)
            {
                case UIStates.Normal: return normal;
                case UIStates.Hovered: return hovered;
                case UIStates.Pressed: return pressed;
                case UIStates.Disabled: return disabled;
                case UIStates.None: return defaultValue;
                default: throw new Exception($"Cant handle {state}");
            }
        }
    }
}
