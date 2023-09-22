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

using Oculus.Interaction.MoveFast;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Modifies the `enabled` state of a list of components, from the `Active` field of the given IActiveState.
    /// Similar to ActiveStateTracker except supports inverting the IActiveState and supports Behaviour types 
    /// rather than MonoBehaviour (e.g. AudioSource) and is about 10% of the code
    /// </summary>
    public class ConditionalComponentsEnabled : ActiveStateObserver
    {
        [SerializeField]
        [Tooltip("Sets the `enabled` field on individual components")]
        List<Behaviour> _behaviours = new List<Behaviour>();

        protected override void Start()
        {
            base.Start();
            HandleActiveStateChanged();
        }

        protected override void HandleActiveStateChanged() => _behaviours.ForEach(x => x.enabled = Active);
    }
}
