// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Profiling;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Handles IActiveState Interface boilerplate, allows inverting directly on the
    /// reference and has a nice property drawer.
    ///
    /// Example Usage:
    ///   [SerializeField]
    ///   private ReferenceActiveState _activeState;
    ///   protected bool Active => _activeState.Active;
    ///   protected virtual void Reset() => _activeState.InjectActiveState(GetComponent<IActiveState>());
    ///   protected virtual void Start() =>  _activeState.AssertNotNull();
    /// </summary>
    [System.Serializable]
    public struct ReferenceActiveState : ISerializationCallbackReceiver, IActiveState
    {
        [SerializeField, Interface(typeof(IActiveState))]
        private MonoBehaviour _activeState;
        [SerializeField]
        bool _invert;
        [SerializeField]
        public bool _useNull;

        private IActiveState ActiveState;
        public bool Active
        {
            get
            {
                if (HasReference)
                {
                    Profiler.BeginSample(ActiveState.GetType().ToString());
                    bool active = ActiveState.Active != _invert;
                    Profiler.EndSample();
                    return active;
                }
                else
                {
                    return _invert;
                }
            }
        }

        public bool HasReference => ActiveState != null;

        public void InjectActiveState(IActiveState activeState)
        {
            ActiveState = activeState;
            _activeState = activeState as MonoBehaviour;
        }

        public void AssertNotNull(string message)
        {
            Assert.IsNotNull(ActiveState, message);
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (ActiveState == null || (ActiveState as MonoBehaviour) == null)
            {
                if (_activeState && _activeState is IActiveState state)
                {
                    ActiveState = state;
                }
                else if (_useNull)
                {
                    ActiveState = new NullActiveState();
                }
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }

        public static implicit operator bool(ReferenceActiveState activeState) => activeState.Active;

        public struct NullActiveState : IActiveState
        {
            public bool Active => true;
        }
    }


#if UNITY_EDITOR
    /// <summary>
    /// Draws ReferenceActiveState on a single line
    /// </summary>
    [CustomPropertyDrawer(typeof(ReferenceActiveState))]
    public class ReferenceActiveStateDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var activeState = property.FindPropertyRelative("_activeState");
            var invert = property.FindPropertyRelative("_invert");

            EditorGUI.BeginProperty(position, label, property);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var activeStateRect = new Rect(position.x, position.y, position.width - 60, position.height);
            var invertToggleRect = new Rect(position.x + (position.width - 60) + 5, position.y, 60, position.height);

            EditorGUI.PropertyField(activeStateRect, activeState, label);
            EditorGUIUtility.labelWidth = 35;
            EditorGUI.PropertyField(invertToggleRect, invert, new GUIContent(activeState.objectReferenceValue != null ? "Invert" : "Value"));

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
#endif
}
