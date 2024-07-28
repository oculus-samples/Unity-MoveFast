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
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Assertions;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Serialized reference to an IProperty of string type
    /// </summary>
    [Serializable]
    struct StringPropertyRef : IProperty<string>, ISerializationCallbackReceiver
    {
        [SerializeField, Interface(typeof(IProperty<string>))]
        private MonoBehaviour _property;
        public IProperty<string> Property;

        public string Value { get => Property.Value; set => Property.Value = value; }

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
            Property = IsNull(Property) ? _property as IProperty<string> : Property;
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
                Property = _property as IProperty<string>;
            }
        }

        private static bool IsNull(IProperty p) => p == null || (p is UnityEngine.Object obj && obj == null);
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(StringPropertyRef), true)]
    class StringPropertyRefDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty _, GUIContent __) => EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect rect, SerializedProperty prop, GUIContent label)
        {
            EditorGUI.PropertyField(rect, prop.FindPropertyRelative("_property"), label);
        }
    }
#endif
}
