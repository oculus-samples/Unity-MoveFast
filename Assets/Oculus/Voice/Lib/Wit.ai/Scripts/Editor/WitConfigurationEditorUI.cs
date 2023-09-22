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
using Facebook.WitAi.Data.Configuration;

namespace Facebook.WitAi
{
    public static class WitConfigurationEditorUI
    {
        // Configuration select
        public static void LayoutConfigurationSelect(ref int configIndex)
        {
            // Refresh configurations if needed
            WitConfiguration[] witConfigs = WitConfigurationUtility.WitConfigs;

            if (witConfigs == null || witConfigs.Length == 0)
            {
                // If no configuration exists, provide a means for the user to create a new one.
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                    
                if (WitEditorUI.LayoutTextButton("New Config"))
                {
                    WitConfigurationUtility.CreateConfiguration("");

                    EditorUtility.FocusProjectWindow();
                }
                    
                GUILayout.FlexibleSpace();
                GUILayout.EndHorizontal();
                
                return;
            }

            // Clamp Config Index
            bool configUpdated = false;
            if (configIndex < 0 || configIndex >= witConfigs.Length)
            {
                configUpdated = true;
                configIndex = Mathf.Clamp(configIndex, 0, witConfigs.Length);
            }

            GUILayout.BeginHorizontal();
            
            // Layout popup
            WitEditorUI.LayoutPopup(WitTexts.Texts.ConfigurationSelectLabel, WitConfigurationUtility.WitConfigNames, ref configIndex, ref configUpdated);

            if (GUILayout.Button("", GUI.skin.GetStyle("IN ObjectField"), GUILayout.Width(15)))
            {
                EditorUtility.FocusProjectWindow();
                EditorGUIUtility.PingObject(witConfigs[configIndex]);
            }
            
            GUILayout.EndHorizontal();
        }
    }
}
