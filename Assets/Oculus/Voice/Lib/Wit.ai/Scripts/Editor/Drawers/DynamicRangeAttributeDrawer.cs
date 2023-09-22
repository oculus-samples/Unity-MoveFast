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

using System.Reflection;
using Facebook.WitAi.Utilities;
using UnityEditor;
using UnityEngine;

namespace Facebook.WitAi.Drawers
{
    [CustomPropertyDrawer(typeof(DynamicRangeAttribute))]
    public class DynamicRangeAttributeDrawer : PropertyDrawer
    {
        private Object _targetObject;
        private float _min;
        private float _max;
        private PropertyInfo _rangePropertyField;
        private object _parentValue;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var value = property.floatValue;
            var attr = attribute as DynamicRangeAttribute;
            var parentPropertyName =
                property.propertyPath.Substring(0, property.propertyPath.IndexOf("."));
            var parentProperty = property.serializedObject.FindProperty(parentPropertyName);

            var targetObject = property.serializedObject.targetObject;
            if(targetObject != _targetObject)
            {
                _targetObject = targetObject;
                var targetObjectClassType = targetObject.GetType();
                var field = targetObjectClassType.GetField(parentProperty.propertyPath,
                    BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (null != field)
                {
                    _parentValue = field.GetValue(targetObject);
                    var parentType = _parentValue.GetType();
                    _rangePropertyField = parentType.GetProperty(attr.RangeProperty,
                        BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                }
            }

            _min = attr.DefaultMin;
            _max = attr.DefaultMax;
            if (null != _rangePropertyField)
            {
                var range = (Vector2) _rangePropertyField.GetValue(_parentValue);
                _min = range.x;
                _max = range.y;
            }

            property.floatValue = EditorGUI.Slider(position, label, property.floatValue,
                _min, _max);

        }
    }
}
