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
using System.Collections.Generic;
using System.Text;
using Facebook.WitAi.Interfaces;
using UnityEngine;

namespace Facebook.WitAi.Configuration
{
    public class WitRequestOptions
    {
        /// <summary>
        /// An interface that provides a list of entities that should be used for nlu resolution.
        /// </summary>
        public IDynamicEntitiesProvider dynamicEntities;

        /// <summary>
        /// The maximum number of intent matches to return
        /// </summary>
        public int nBestIntents = -1;

        /// <summary>
        /// The tag for snapshot
        /// </summary>
        public string tag;

        /// <summary>
        /// A GUID - For internal use
        /// </summary>
        public string requestID = Guid.NewGuid().ToString();

        /// <summary>
        /// Additional parameters to be used for custom
        /// implementation overrides.
        /// </summary>
        public Dictionary<string, string> additionalParameters = new Dictionary<string, string>();

        /// <summary>
        /// Callback for completion
        /// </summary>
        public Action<WitRequest> onResponse;

        // Get json string
        public string ToJsonString()
        {
            // Get default json
            string results = JsonUtility.ToJson(this);

            // Append parameters before final }
            StringBuilder parameters = new StringBuilder();
            foreach (var key in additionalParameters.Keys)
            {
                string value = additionalParameters[key].Replace("\"", "\\\"");
                parameters.Append($",\"{key}\":\"{value}\"");
            }
            results = results.Insert(results.Length - 1, parameters.ToString());

            // Return json
            return results;
        }
    }
}
