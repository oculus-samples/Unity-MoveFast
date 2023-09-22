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

using UnityEditor;
using System.Reflection;

namespace Facebook.WitAi.Windows
{
    public class WitApplicationPropertyDrawer : WitPropertyDrawer
    {
        // Whether to use a foldout
        protected override bool FoldoutEnabled => false;
        // Use name value for title if possible
        protected override string GetLocalizedText(SerializedProperty property, string key)
        {
            // Determine by ids
            switch (key)
            {
                case LocalizedTitleKey:
                    return WitTexts.Texts.ConfigurationApplicationTabLabel;
                case LocalizedMissingKey:
                    return WitTexts.Texts.ConfigurationApplicationMissingLabel;
                case "name":
                    return WitTexts.Texts.ConfigurationApplicationNameLabel;
                case "id":
                    return WitTexts.Texts.ConfigurationApplicationIdLabel;
                case "lang":
                    return WitTexts.Texts.ConfigurationApplicationLanguageLabel;
                case "isPrivate":
                    return WitTexts.Texts.ConfigurationApplicationPrivateLabel;
                case "createdAt":
                    return WitTexts.Texts.ConfigurationApplicationCreatedLabel;
            }

            // Default to base
            return base.GetLocalizedText(property, key);
        }
        // Skip wit configuration field
        protected override bool ShouldLayoutField(SerializedProperty property, FieldInfo subfield)
        {
            switch (subfield.Name)
            {
                case "witConfiguration":
                    return false;
            }
            return base.ShouldLayoutField(property, subfield);
        }
    }
}
