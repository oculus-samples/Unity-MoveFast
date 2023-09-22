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
using Facebook.WitAi.Dictation;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Facebook.WitAi.Utilities
{
    [Serializable]
    public struct DictationServiceReference
    {
        [SerializeField] internal DictationService dictationService;

        public DictationService DictationService
        {
            get
            {
                if (!dictationService)
                {
                    DictationService[] services = Resources.FindObjectsOfTypeAll<DictationService>();
                    if (services != null)
                    {
                        // Set as first instance that isn't a prefab
                        dictationService = Array.Find(services, (o) => o.gameObject.scene.rootCount != 0);
                    }
                }

                return dictationService;
            }
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DictationServiceReference))]
    public class DictationServiceReferenceDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var refProp = property.FindPropertyRelative("DictationService");
            var reference = refProp.objectReferenceValue as DictationService;
            var dictationServices = GameObject.FindObjectsOfType<DictationService>();
            var dictationServiceNames = new string[dictationServices.Length + 1];
            int index = 0;
            dictationServiceNames[0] = "Autodetect";
            if (dictationServices.Length == 1)
            {
                dictationServiceNames[0] = $"{dictationServiceNames[0]} - {dictationServices[0].name}";
            }
            for (int i = 0; i < dictationServices.Length; i++)
            {
                dictationServiceNames[i + 1] = dictationServices[i].name;
                if (dictationServices[i] == reference)
                {
                    index = i + 1;
                }
            }
            EditorGUI.BeginProperty(position, label, property);
            var updatedIndex = EditorGUI.Popup(position, index, dictationServiceNames);
            if (index != updatedIndex)
            {
                if (updatedIndex > 0)
                {
                    refProp.objectReferenceValue = dictationServices[updatedIndex - 1];
                }
                else
                {
                    refProp.objectReferenceValue = null;
                }

                property.serializedObject.ApplyModifiedProperties();
            }
            EditorGUI.EndProperty();
        }
    }
#endif
}
