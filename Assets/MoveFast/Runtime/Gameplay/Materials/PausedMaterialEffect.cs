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

using System.Collections.Generic;
using UnityEngine;
using BlendMode = Oculus.Interaction.MoveFast.StandardShaderUtils.BlendMode;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Changes the punch target's material's alpha while the game is paused
    /// </summary>
    public class PausedMaterialEffect : ActiveStateObserver
    {
        [Header("Materials")]
        [SerializeField]
        private List<Material> _baseMats = new List<Material>();
        [SerializeField]
        private List<Material> _targetMats = new List<Material>();

        protected override void Start()
        {
            base.Start();
            HandleActiveStateChanged();
        }

        private void OnDestroy() => SetKnockedBack(false);

        protected override void HandleActiveStateChanged()
        {
            SetKnockedBack(Active);
        }

        private void SetKnockedBack(bool knockedBack)
        {
            var alpha = knockedBack ? 0.1f : 1f;
            var blendMode = knockedBack ? BlendMode.Fade : BlendMode.Opaque;

            for (int i = 0; i < _baseMats.Count; i++)
            {
                SetMaterialAlpha(_baseMats[i], alpha);
            }

            for (int i = 0; i < _targetMats.Count; i++)
            {
                StandardShaderUtils.ChangeRenderMode(_targetMats[i], blendMode);
                SetMaterialAlpha(_targetMats[i], alpha);
            }
        }

        private static void SetMaterialAlpha(Material material, float alpha)
        {
            var color = material.color;
            color.a = alpha;
            material.color = color;
        }
    }
}
