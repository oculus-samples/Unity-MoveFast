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
using Facebook.WitAi.Lib;
using UnityEngine;

namespace Facebook.WitAi.Data.Traits
{
    [Serializable]
    public class WitTrait : WitConfigurationData
    {
        [SerializeField] public string id;
        [SerializeField] public string name;
        [SerializeField] public WitTraitValue[] values;

        #if UNITY_EDITOR
        protected override WitRequest OnCreateRequest()
        {
            return witConfiguration.GetTraitRequest(name);
        }

        public override void UpdateData(WitResponseNode traitWitResponse)
        {
            id = traitWitResponse["id"].Value;
            name = traitWitResponse["name"].Value;
            var valueArray = traitWitResponse["values"].AsArray;
            var n = valueArray.Count;
            values = new WitTraitValue[n];
            for (int i = 0; i < n; i++) {
                values[i] = WitTraitValue.FromJson(valueArray[i]);
            }
        }

        public static WitTrait FromJson(WitResponseNode traitWitResponse)
        {
            var trait = new WitTrait();
            trait.UpdateData(traitWitResponse);
            return trait;
        }
        #endif
    }
}
