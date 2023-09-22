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
using System.Collections.Generic;
using System.Linq;

namespace Meta.Conduit
{
    /// <summary>
    /// Marks a parameter as a Conduit parameter to be supplied when the callback method is called.
    /// This is not required, but allows the addition of more information about parameters to improve the quality of
    /// intent recognition and entity resolution. 
    /// </summary>
    [AttributeUsage(System.AttributeTargets.Parameter)]
    public class ConduitParameterAttribute : Attribute
    {
        public ConduitParameterAttribute(params string[] aliases)
        {
            this.Aliases = aliases.ToList();
        }

        /// <summary>
        /// The names that refer to this parameter.
        /// </summary>
        public List<string> Aliases { get; }
    }
}
