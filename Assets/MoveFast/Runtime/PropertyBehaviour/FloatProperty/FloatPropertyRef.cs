// Copyright (c) Meta Platforms, Inc. and affiliates.

using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Assertions;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Serializable reference to an IProperty of float type
    /// </summary>
    [Serializable]
    struct FloatPropertyRef : IProperty<float>, ISerializationCallbackReceiver
    {
        [SerializeField, Interface(typeof(IProperty<float>))]
        private MonoBehaviour _property;
        public IProperty<float> Property;

        public float Value { get => Property.Value; set => Property.Value = value; }

        public event Action WhenChanged
        {
            add => Property.WhenChanged += value;
            remove
            {
                if (Property != null) { Property.WhenChanged -= value; }
            }
        }

        public void AssertNotNull()
        {
            Property = IsNull(Property) ? _property as IProperty<float> : Property;
            Assert.IsNotNull(Property);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (_property == null && Property is MonoBehaviour mono && mono != null)
            {
                _property = mono;
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (_property != null)
            {
                Property = _property as IProperty<float>;
            }
        }

        private static bool IsNull(IProperty p) => p == null || (p is UnityEngine.Object obj && obj == null);

#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(FloatPropertyRef), true)]
        class FloatPropertyRefDrawer : PropertyDrawer
        {
            public override float GetPropertyHeight(SerializedProperty _, GUIContent __) => EditorGUIUtility.singleLineHeight;

            public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
            {
                EditorGUI.PropertyField(rect, prop.FindPropertyRelative("_property"), label);
            }
        }
#endif
    }
}
