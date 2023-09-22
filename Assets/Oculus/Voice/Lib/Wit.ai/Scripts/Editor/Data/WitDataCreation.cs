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

using System.Globalization;
using System.Text.RegularExpressions;
using Facebook.WitAi.Configuration;
using Facebook.WitAi.Data.Configuration;
using UnityEditor;
using UnityEngine;


namespace Facebook.WitAi.Data
{
    public class WitDataCreation
    {
        const string PATH_KEY = "Facebook::Wit::ValuePath";

        public static WitConfiguration FindDefaultWitConfig()
        {
            string[] guids = AssetDatabase.FindAssets("t:WitConfiguration");
            if(guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return AssetDatabase.LoadAssetAtPath<WitConfiguration>(path);
            }

            return null;
        }

        public static void AddWitToScene()
        {
            var witGo = new GameObject
            {
                name = "Wit"
            };
            var wit = witGo.AddComponent<Wit>();
            var runtimeConfiguration = new WitRuntimeConfiguration()
            {
                witConfiguration = FindDefaultWitConfig()
            };
            wit.RuntimeConfiguration = runtimeConfiguration;
        }

        public static WitStringValue CreateStringValue(string path)
        {
            var asset = ScriptableObject.CreateInstance<WitStringValue>();
            CreateValueAsset("Create String Value", path, asset);
            return asset;
        }

        public static WitFloatValue CreateFloatValue(string path)
        {
            var asset = ScriptableObject.CreateInstance<WitFloatValue>();
            CreateValueAsset("Create Float Value", path, asset);
            return asset;
        }

        public static WitIntValue CreateIntValue(string path)
        {
            var asset = ScriptableObject.CreateInstance<WitIntValue>();
            CreateValueAsset("Create Int Value", path, asset);
            return asset;
        }

        private static void CreateValueAsset(string label, string path, WitValue asset)
        {
            asset.path = path;
            var saveDir = EditorPrefs.GetString(PATH_KEY, Application.dataPath);
            string name;

            if (!string.IsNullOrEmpty(path))
            {
                name = Regex.Replace(path, @"\[[\]0-9]+", "");
                name = name.Replace(".", " ");
                name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name);
            }
            else
            {
                name = asset.GetType().Name;
            }

            var filePath = EditorUtility.SaveFilePanel(label, saveDir, name, "asset");
            if (!string.IsNullOrEmpty(filePath))
            {
                EditorPrefs.SetString(PATH_KEY, filePath);
                if (filePath.StartsWith(Application.dataPath))
                {
                    filePath = filePath.Substring(Application.dataPath.Length - 6);
                }
                AssetDatabase.CreateAsset(asset, filePath);
                AssetDatabase.SaveAssets();
            }
        }
    }
}
