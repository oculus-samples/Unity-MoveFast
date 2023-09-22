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
using Facebook.WitAi.Interfaces;
using Facebook.WitAi.Lib;

namespace Facebook.WitAi.Data.Entities
{
    [Serializable]
    public class WitDynamicEntity : IDynamicEntitiesProvider
    {
        public string entity;
        public List<WitEntityKeyword> keywords = new List<WitEntityKeyword>();

        public WitDynamicEntity()
        {
        }

        public WitDynamicEntity(string entity, WitEntityKeyword keyword)
        {
            this.entity = entity;
            this.keywords.Add(keyword);
        }

        public WitDynamicEntity(string entity, params string[] keywords)
        {
            this.entity = entity;
            foreach (var keyword in keywords)
            {
                this.keywords.Add(new WitEntityKeyword(keyword));
            }
        }

        public WitDynamicEntity(string entity, Dictionary<string, List<string>> keywordsToSynonyms)
        {
            this.entity = entity;

            foreach (var synonym in keywordsToSynonyms)
            {
                keywords.Add(new WitEntityKeyword()
                {
                    keyword = synonym.Key,
                    synonyms = synonym.Value
                });

            }
        }

        public WitResponseArray AsJson
        {
            get
            {
                WitResponseArray synonymArray = new WitResponseArray();
                foreach (var keyword in keywords)
                {
                    synonymArray.Add(keyword.AsJson);
                }

                return synonymArray;
            }
        }

        public WitDynamicEntities GetDynamicEntities()
        {
            return new WitDynamicEntities()
            {
                entities = new List<WitDynamicEntity>
                {
                    this
                }
            };
        }
    }
}
