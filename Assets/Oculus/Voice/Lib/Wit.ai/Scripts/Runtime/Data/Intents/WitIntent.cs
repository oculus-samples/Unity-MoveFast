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
using Facebook.WitAi.Configuration;
using Facebook.WitAi.Data.Entities;
using Facebook.WitAi.Lib;
using UnityEngine;

namespace Facebook.WitAi.Data.Intents
{

    [Serializable]
    public class WitIntent : WitConfigurationData
    {
        [SerializeField] public string id;
        [SerializeField] public string name;
        [SerializeField] public WitEntity[] entities;

        public static class Fields
        {
            public const string ID = "id";
            public const string NAME = "name";
            public const string CONFIDENCE = "confidence";
        }

        #if UNITY_EDITOR
        public static class EditorFields
        {
            public const string ENTITIES = "entities";
        }

        protected override WitRequest OnCreateRequest()
        {
            return witConfiguration.GetIntentRequest(name);
        }

        public override void UpdateData(WitResponseNode intentWitResponse)
        {
            id = intentWitResponse[Fields.ID].Value;
            name = intentWitResponse[Fields.NAME].Value;
            var entityArray = intentWitResponse[EditorFields.ENTITIES].AsArray;
            var n = entityArray.Count;
            entities = new WitEntity[n];
            for (int i = 0; i < n; i++)
            {
                entities[i] = WitEntity.FromJson(entityArray[i]);
                entities[i].witConfiguration = witConfiguration;
            }
        }

        public static WitIntent FromJson(WitResponseNode intentWitResponse)
        {
            var intent = new WitIntent();
            intent.UpdateData(intentWitResponse);
            return intent;
        }
        #endif
    }
}
