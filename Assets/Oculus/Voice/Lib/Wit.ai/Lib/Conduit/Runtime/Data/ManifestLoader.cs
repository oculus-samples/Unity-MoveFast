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

using Meta.Wit.LitJson;
using UnityEngine;

namespace Meta.Conduit
{
    /// <summary>
    /// Loads the manifest and resolves its actions so they can be used during dispatching.
    /// </summary>
    class ManifestLoader : IManifestLoader
    {
        /// <summary>
        /// Loads the manifest from file and into a <see cref="Manifest"/> structure.
        /// </summary>
        /// <param name="filePath">The path to the manifest file.</param>
        /// <returns>The loaded manifest object.</returns>
        public Manifest LoadManifest(string manifestLocalPath)
        {
            Debug.Log($"Loaded Conduit manifest from Resources/{manifestLocalPath}");
            int extIndex = manifestLocalPath.LastIndexOf('.');
            string ignoreEnd = extIndex == -1 ? manifestLocalPath : manifestLocalPath.Substring(0, extIndex);
            TextAsset jsonFile = Resources.Load<TextAsset>(ignoreEnd);
            if (jsonFile == null)
            {
                Debug.LogError($"Conduit Error - No Manifest found at Resources/{manifestLocalPath}");
                return null;
            }

            string rawJson = jsonFile.text;
            var manifest = JsonMapper.ToObject<Manifest>(rawJson);
            if (manifest.ResolveActions())
            {
                Debug.Log($"Successfully Loaded Conduit manifest");
            }
            else
            {
                Debug.LogError($"Fail to resolve actions from Conduit manifest");
            }

            return manifest;
        }
    }
}
