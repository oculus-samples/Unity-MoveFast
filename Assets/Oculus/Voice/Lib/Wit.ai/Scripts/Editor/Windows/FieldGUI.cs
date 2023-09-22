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
using System.Reflection;
using UnityEditor;

namespace Facebook.WitAi.Windows
{
    public class FieldGUI
    {
        // Base type
        public Type baseType { get; private set; }
        // Fields
        public FieldInfo[] fields { get; private set; }

        /// <summary>
        /// Custom gui layout callback, returns true if field is
        /// </summary>
        public Func<FieldInfo, bool> onCustomGuiLayout;

        /// <summary>
        /// Custom gui layout callback, returns true if field is
        /// </summary>
        public Action onAdditionalGuiLayout;

        // Refresh field list
        public void RefreshFields(Type newBaseType)
        {
            // Set base type
            baseType = newBaseType;

            // Obtain all public, instance fields
            fields = GetFields(baseType);
        }

        // Obtain all public, instance fields
        public static FieldInfo[] GetFields(Type newBaseType)
        {
            // Results
            FieldInfo[] results = newBaseType.GetFields(BindingFlags.Public | BindingFlags.Instance);

            // Sort parent class fields to top
            Array.Sort(results, (f1, f2) =>
            {
                if (f1.DeclaringType != f2.DeclaringType)
                {
                    if (f1.DeclaringType == newBaseType)
                    {
                        return 1;
                    }
                    if (f2.DeclaringType == newBaseType)
                    {
                        return -1;
                    }
                }
                return 0;
            });

            // Return results
            return results;
        }

        // Gui Layout
        public void OnGuiLayout(SerializedObject serializedObject)
        {
            // Ignore without object
            if (serializedObject == null || serializedObject.targetObject == null)
            {
                return;
            }
            // Attempt a setup if needed
            Type desType = serializedObject.targetObject.GetType();
            if (baseType != desType || fields == null)
            {
                RefreshFields(desType);
            }
            // Ignore
            if (fields == null)
            {
                return;
            }

            // Iterate all fields
            foreach (var field in fields)
            {
                // Custom handle
                if (onCustomGuiLayout != null && onCustomGuiLayout(field))
                {
                    continue;
                }

                // Default layout
                var property = serializedObject.FindProperty(field.Name);
                EditorGUILayout.PropertyField(property);
            }

            // Additional items
            onAdditionalGuiLayout?.Invoke();

            // Apply
            serializedObject.ApplyModifiedProperties();
        }
    }
}
