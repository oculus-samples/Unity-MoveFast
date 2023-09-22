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

using Facebook.WitAi.Configuration;
using Facebook.WitAi.Interfaces;
using UnityEngine;

namespace Facebook.WitAi
{
    public class Wit : VoiceService, IWitRuntimeConfigProvider
    {
        [SerializeField] private WitRuntimeConfiguration witRuntimeConfiguration;

        public WitRuntimeConfiguration RuntimeConfiguration
        {
            get => witRuntimeConfiguration;
            set => witRuntimeConfiguration = value;
        }

        private WitService witService;

        #region Voice Service Properties
        public override bool Active => null != witService && witService.Active;
        public override bool IsRequestActive => null != witService && witService.IsRequestActive;
        public override ITranscriptionProvider TranscriptionProvider
        {
            get => witService.TranscriptionProvider;
            set => witService.TranscriptionProvider = value;
        }
        public override bool MicActive => null != witService && witService.MicActive;
        protected override bool ShouldSendMicData => witRuntimeConfiguration.sendAudioToWit ||
                                                  null == TranscriptionProvider;
        #endregion

        #region Voice Service Methods

        public override void Activate(string text, WitRequestOptions requestOptions)
        {
            witService.Activate(text, requestOptions);
        }

        public override void Activate(WitRequestOptions options)
        {
            witService.Activate(options);
        }

        public override void ActivateImmediately(WitRequestOptions options)
        {
            witService.ActivateImmediately(options);
        }

        public override void Deactivate()
        {
            witService.Deactivate();
        }

        public override void DeactivateAndAbortRequest()
        {
            witService.DeactivateAndAbortRequest();
        }

        #endregion

        protected override void Awake()
        {
            base.Awake();

            // WitService is 1:1 tied to a VoiceService. In the event there
            // are multiple voice services on a game object this will ensure
            // that this component has its own dedicated WitService
            witService = gameObject.AddComponent<WitService>();
            witService.VoiceEventProvider = this;
            witService.ConfigurationProvider = this;
        }
    }
}
