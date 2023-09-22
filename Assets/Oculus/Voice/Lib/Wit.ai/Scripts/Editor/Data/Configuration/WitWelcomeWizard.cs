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
using UnityEngine;
using Facebook.WitAi.Data.Configuration;

namespace Facebook.WitAi.Windows
{
    public class WitWelcomeWizard : WitScriptableWizard
    {
        protected string serverToken;
        public Action<WitConfiguration> successAction;

        protected override Texture2D HeaderIcon => WitTexts.HeaderIcon;
        protected override GUIContent Title => WitTexts.SetupTitleContent;
        protected override string ButtonLabel => WitTexts.Texts.SetupSubmitButtonLabel;
        protected override string ContentSubheaderLabel => WitTexts.Texts.SetupSubheaderLabel;

        protected override void OnEnable()
        {
            base.OnEnable();
            serverToken = string.Empty;
            WitAuthUtility.ServerToken = serverToken;
        }
        protected override bool DrawWizardGUI()
        {
            // Layout base
            base.DrawWizardGUI();
            // True if valid server token
            return WitConfigurationUtility.IsServerTokenValid(serverToken);
        }
        protected override void LayoutFields()
        {
            string serverTokenLabelText = WitTexts.Texts.SetupServerTokenLabel;
            serverTokenLabelText = serverTokenLabelText.Replace(WitStyles.WitLinkKey, WitStyles.WitLinkColor);
            if (GUILayout.Button(serverTokenLabelText, WitStyles.Label))
            {
                Application.OpenURL(WitTexts.GetAppURL("", WitTexts.WitAppEndpointType.Settings));
            }
            bool updated = false;
            WitEditorUI.LayoutPasswordField(null, ref serverToken, ref updated);
        }
        protected override void OnWizardCreate()
        {
            ValidateAndClose();
        }
        protected virtual void ValidateAndClose()
        {
            WitAuthUtility.ServerToken = serverToken;
            if (WitAuthUtility.IsServerTokenValid())
            {
                // Create configuration
                int index = CreateConfiguration(serverToken);
                if (index != -1)
                {
                    // Complete
                    Close();
                    WitConfiguration c = WitConfigurationUtility.WitConfigs[index];
                    if (successAction == null)
                    {
                        WitWindowUtility.OpenConfigurationWindow(c);
                    }
                    else
                    {
                        successAction(c);
                    }
                }
            }
            else
            {
                throw new ArgumentException(WitTexts.Texts.SetupSubmitFailLabel);
            }
        }
        protected virtual int CreateConfiguration(string newToken)
        {
            return WitConfigurationUtility.CreateConfiguration(newToken);
        }
    }
}
