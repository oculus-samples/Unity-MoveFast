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

using System.Collections.Generic;
using UnityEditor;

namespace Facebook.WitAi.Utilities
{
    public static class AssetDatabaseUtility
    {
        // Find Unity asset
        public static T FindUnityAsset<T>(string filter) where T : UnityEngine.Object
        {
            T[] results = FindUnityAssets<T>(filter, true);
            if (results != null && results.Length > 0)
            {
                return results[0];
            }
            return null;
        }

        // Get all unity objects matching the name
        public static T[] FindUnityAssets<T>(string filter, bool ignoreAdditional = false)
            where T : UnityEngine.Object
        {
            List<T> results = new List<T>();
            string[] guids = AssetDatabase.FindAssets(filter);
            if (guids != null && guids.Length > 0)
            {
                foreach (var guid in guids)
                {
                    string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                    T asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                    if (asset != null && !results.Contains(asset))
                    {
                        results.Add(asset);
                        if (ignoreAdditional)
                        {
                            break;
                        }
                    }
                }
            }
            return results.ToArray();
        }
    }
}
