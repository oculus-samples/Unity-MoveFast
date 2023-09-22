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

using UnityEditor;
using UnityEngine;
using System.Reflection;

namespace Facebook.WitAi.Windows
{
    public class WitIntentPropertyDrawer : WitPropertyDrawer
    {
        // Use name value for title if possible
        protected override string GetLocalizedText(SerializedProperty property, string key)
        {
            // Determine by ids
            switch (key)
            {
                case LocalizedTitleKey:
                    string title = GetFieldStringValue(property, "name");
                    if (!string.IsNullOrEmpty(title))
                    {
                        return title;
                    }
                    break;
                case "id":
                    return WitTexts.Texts.ConfigurationIntentsIdLabel;
                case "entities":
                    return WitTexts.Texts.ConfigurationIntentsEntitiesLabel;
            }

            // Default to base
            return base.GetLocalizedText(property, key);
        }
        // Layout entity override
        protected override void LayoutPropertyField(FieldInfo subfield, SerializedProperty subfieldProperty, GUIContent labelContent, bool canEdit)
        {
            // Handle all the same except entities
            if (canEdit || !string.Equals(subfield.Name, "entities"))
            {
                base.LayoutPropertyField(subfield, subfieldProperty, labelContent, canEdit);
                return;
            }

            // Entity foldout
            subfieldProperty.isExpanded = WitEditorUI.LayoutFoldout(labelContent, subfieldProperty.isExpanded);
            if (subfieldProperty.isExpanded)
            {
                EditorGUI.indentLevel++;
                if (subfieldProperty.arraySize == 0)
                {
                    WitEditorUI.LayoutErrorLabel(WitTexts.Texts.ConfigurationEntitiesMissingLabel);
                }
                else
                {
                    for (int i = 0; i < subfieldProperty.arraySize; i++)
                    {
                        SerializedProperty entityProp = subfieldProperty.GetArrayElementAtIndex(i);
                        string entityPropName = entityProp.FindPropertyRelative("name").stringValue;
                        WitEditorUI.LayoutLabel(entityPropName);
                    }
                }
                EditorGUI.indentLevel--;
            }
        }
        // Determine if should layout field
        protected override bool ShouldLayoutField(SerializedProperty property, FieldInfo subfield)
        {
            switch (subfield.Name)
            {
                case "name":
                    return false;
            }
            return base.ShouldLayoutField(property, subfield);
        }
    }
}
