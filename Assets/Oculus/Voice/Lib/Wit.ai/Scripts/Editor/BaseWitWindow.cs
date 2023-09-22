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

using Facebook.WitAi.Data.Configuration;
using UnityEditor;
using UnityEngine;

namespace Facebook.WitAi.Windows
{
    public abstract class BaseWitWindow : EditorWindow
    {
        // Scroll offset
        private Vector2 ScrollOffset;

        // Override values
        protected abstract GUIContent Title { get; }
        protected virtual Texture2D HeaderIcon => WitTexts.HeaderIcon;
        protected virtual string HeaderUrl => WitTexts.WitUrl;

        // Window open
        protected virtual void OnEnable()
        {
            titleContent = Title;
            WitConfigurationUtility.ReloadConfigurationData();
        }
        // Window close
        protected virtual void OnDisable()
        {
            ScrollOffset = Vector2.zero;
        }
        // Handle Layout
        protected virtual void OnGUI()
        {
            Vector2 size;
            WitEditorUI.LayoutWindow(titleContent.text, HeaderIcon, HeaderUrl, LayoutContent, ref ScrollOffset, out size);
        }
        // Draw content of window
        protected abstract void LayoutContent();
    }
}
