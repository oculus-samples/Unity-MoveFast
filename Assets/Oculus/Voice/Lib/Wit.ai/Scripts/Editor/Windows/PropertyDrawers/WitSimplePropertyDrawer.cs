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
using UnityEditor;

namespace Facebook.WitAi.Windows
{
    // Handles layout of very simple property drawer
    public abstract class WitSimplePropertyDrawer : PropertyDrawer
    {
        // Get field names
        protected abstract string GetKeyFieldName();
        protected abstract string GetValueFieldName();

        // Remove padding
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 0;
        }
        // Handles gui layout
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            string keyText = GetFieldStringValue(property, GetKeyFieldName());
            string valueText = GetFieldStringValue(property, GetValueFieldName());
            WitEditorUI.LayoutKeyLabel(keyText, valueText);
        }
        // Get subfield value
        protected virtual string GetFieldStringValue(SerializedProperty property, string fieldName)
        {
            SerializedProperty subfieldProperty = property.FindPropertyRelative(fieldName);
            string result = GetFieldStringValue(subfieldProperty);
            if (string.IsNullOrEmpty(result))
            {
                result = fieldName;
            }
            return result;
        }
        // Get subfield value
        protected virtual string GetFieldStringValue(SerializedProperty subfieldProperty)
        {
            // Supported types
            switch (subfieldProperty.type)
            {
                case "string":
                    return subfieldProperty.stringValue;
                case "int":
                    return subfieldProperty.intValue.ToString();
                case "bool":
                    return subfieldProperty.boolValue.ToString();
            }
            // No others are currently supported
            return string.Empty;
        }
    }
}
