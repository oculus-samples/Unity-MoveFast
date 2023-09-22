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

using Facebook.WitAi.Interfaces;
using UnityEngine;

namespace Facebook.WitAi.Data.Entities
{
    /// <summary>
    /// Singleton registry for tracking any objects owned defined in entities in
    /// a scene
    /// </summary>
    public class DynamicEntityKeywordRegistry : MonoBehaviour, IDynamicEntitiesProvider
    {
        private static DynamicEntityKeywordRegistry instance;

        private WitDynamicEntities entities = new WitDynamicEntities();

        public static bool HasDynamicEntityRegistry => Instance;

        /// <summary>
        /// Gets the instance in the scene if there is one
        /// </summary>
        public static DynamicEntityKeywordRegistry Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType<DynamicEntityKeywordRegistry>();
                }

                return instance;
            }
        }

        private void OnEnable()
        {
            instance = this;
        }

        private void OnDisable()
        {
            instance = null;
        }

        public void RegisterDynamicEntity(string entity, WitEntityKeyword keyword)
        {
            entities.AddKeyword(entity, keyword);
        }

        public void UnregisterDynamicEntity(string entity, WitEntityKeyword keyword)
        {
            entities.RemoveKeyword(entity, keyword);
        }

        public WitDynamicEntities GetDynamicEntities()
        {
            return entities;
        }
    }
}
