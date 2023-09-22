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
using System.Linq;
using Facebook.WitAi.Lib;

namespace Facebook.WitAi.Data.Entities
{
    [Serializable]
    public class WitEntityKeyword
    {
        public string keyword;
        public List<string> synonyms = new List<string>();

        public WitEntityKeyword() {}

        public WitEntityKeyword(string keyword)
        {
            this.keyword = keyword;
        }

        public WitEntityKeyword(string keyword, params string[] synonyms)
        {
            this.keyword = keyword;
            this.synonyms.AddRange(synonyms);
        }

        public WitEntityKeyword(string keyword, IEnumerable<string> synonyms)
        {
            this.keyword = keyword;
            this.synonyms.AddRange(synonyms);
        }

        public WitResponseClass AsJson
        {
            get
            {
                var synonymArray = new WitResponseArray();

                foreach (var synonym in synonyms)
                {
                    synonymArray.Add(synonym);
                }

                return new WitResponseClass
                {
                    {"keyword", new WitResponseData(keyword)},
                    {"synonyms", synonymArray}
                };
            }
        }

        public static WitEntityKeyword FromJson(WitResponseNode keywordNode)
        {
            return new WitEntityKeyword()
            {
                keyword = keywordNode["keyword"],
                synonyms = keywordNode["synonyms"].AsStringArray.ToList()
            };
        }
    }
}
