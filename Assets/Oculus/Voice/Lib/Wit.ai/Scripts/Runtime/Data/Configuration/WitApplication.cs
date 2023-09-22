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

namespace Facebook.WitAi.Data.Configuration
{

    [Serializable]
    public class WitApplication : WitConfigurationData
    {
        [SerializeField] public string name;
        [SerializeField] public string id;
        [SerializeField] public string lang;
        [SerializeField] public bool isPrivate;
        [SerializeField] public string createdAt;

        #if UNITY_EDITOR
        protected override WitRequest OnCreateRequest()
        {
            return witConfiguration.GetAppRequest(id);
        }

        public override void UpdateData(WitResponseNode appWitResponse)
        {
            id = appWitResponse["id"].Value;
            name = appWitResponse["name"].Value;
            lang = appWitResponse["lang"].Value;
            isPrivate = appWitResponse["private"].AsBool;
            createdAt = appWitResponse["created_at"].Value;
        }

        public static WitApplication FromJson(WitResponseNode appWitResponse)
        {
            var app = new WitApplication();
            app.UpdateData(appWitResponse);
            return app;
        }
        #endif
    }
}
