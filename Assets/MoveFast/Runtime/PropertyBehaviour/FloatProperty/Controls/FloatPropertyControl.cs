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

using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Maps a FloatProperty to some other value
    /// </summary>
    public abstract class FloatPropertyControl : MonoBehaviour
    {
        [SerializeField]
        FloatPropertyRef _floatProperty;

        protected virtual void Awake()
        {
            _floatProperty.WhenChanged += UpdateControl;
            UpdateControl();
        }

        protected virtual void OnDestroy()
        {
            _floatProperty.WhenChanged -= UpdateControl;
        }

        public void UpdateControl() => UpdateControl(_floatProperty.Value);

        protected abstract void UpdateControl(float value);

        protected void UpdateProperty(float value)
        {
            _floatProperty.WhenChanged -= UpdateControl;
            _floatProperty.Value = value;
            _floatProperty.WhenChanged += UpdateControl;
        }
    }
}
