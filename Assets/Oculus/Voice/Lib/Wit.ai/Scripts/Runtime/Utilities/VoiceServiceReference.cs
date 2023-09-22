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
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Facebook.WitAi.Utilities
{
    [Serializable]
    public struct VoiceServiceReference
    {
        [SerializeField] internal VoiceService voiceService;

        public VoiceService VoiceService
        {
            get
            {
                if (!voiceService)
                {
                    VoiceService[] services = Resources.FindObjectsOfTypeAll<VoiceService>();
                    if (services != null)
                    {
                        // Set as first instance that isn't a prefab
                        voiceService = Array.Find(services, (o) => o.gameObject.scene.rootCount != 0);
                    }
                }

                return voiceService;
            }
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(VoiceServiceReference))]
    public class VoiceServiceReferenceDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var refProp = property.FindPropertyRelative("voiceService");
            var reference = refProp.objectReferenceValue as VoiceService;
            var voiceServices = GameObject.FindObjectsOfType<VoiceService>();
            var voiceServiceNames = new string[voiceServices.Length + 1];
            int index = 0;
            voiceServiceNames[0] = "Autodetect";
            if (voiceServices.Length == 1)
            {
                voiceServiceNames[0] = $"{voiceServiceNames[0]} - {voiceServices[0].name}";
            }
            for (int i = 0; i < voiceServices.Length; i++)
            {
                voiceServiceNames[i + 1] = voiceServices[i].name;
                if (voiceServices[i] == reference)
                {
                    index = i + 1;
                }
            }
            EditorGUI.BeginProperty(position, label, property);
            var updatedIndex = EditorGUI.Popup(position, index, voiceServiceNames);
            if (index != updatedIndex)
            {
                if (updatedIndex > 0)
                {
                    refProp.objectReferenceValue = voiceServices[updatedIndex - 1];
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
