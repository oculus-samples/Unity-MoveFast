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
using UnityEngine;

namespace Facebook.WitAi.Windows
{
    public abstract class WitScriptableWizard : ScriptableWizard
    {
        protected Vector2 scrollOffset;

        protected virtual Texture2D HeaderIcon => WitTexts.HeaderIcon;
        protected virtual string HeaderUrl => WitTexts.WitUrl;

        protected abstract GUIContent Title { get; }
        protected abstract string ButtonLabel { get; }
        protected virtual string ContentHeaderLabel => Title.text;
        protected abstract string ContentSubheaderLabel { get; }

        protected virtual void OnEnable()
        {
            createButtonName = ButtonLabel;
        }
        protected override bool DrawWizardGUI()
        {
            // Reapply title if needed
            if (titleContent != Title)
            {
                titleContent = Title;
            }

            // Layout window
            Vector2 size = Vector2.zero;
            WitEditorUI.LayoutWindow(ContentHeaderLabel, HeaderIcon, HeaderUrl, LayoutContent, ref scrollOffset, out size);

            // Set wizard to max width
            size.x = WitStyles.WindowMaxWidth;
            // Wizards add additional padding
            size.y += 70f;

            // Clamp wizard sizes
            maxSize = minSize = size;

            // True if valid server token
            return false;
        }
        protected virtual void LayoutContent()
        {
            if (!string.IsNullOrEmpty(ContentSubheaderLabel))
            {
                WitEditorUI.LayoutSubheaderLabel(ContentSubheaderLabel);
                GUILayout.Space(WitStyles.HeaderPaddingBottom * 2f);
            }
            GUILayout.BeginHorizontal();
            GUILayout.Space(WitStyles.WizardFieldPadding);
            GUILayout.BeginVertical();
            LayoutFields();
            GUILayout.EndVertical();
            GUILayout.Space(WitStyles.WizardFieldPadding);
            GUILayout.EndHorizontal();
        }
        protected abstract void LayoutFields();
        protected virtual void OnWizardCreate()
        {

        }
    }
}
