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
using System.Reflection;

namespace Meta.Conduit
{
    /// <summary>
    /// Holds the details required to invoke a method at runtime.
    /// </summary>
    internal class InvocationContext
    {
        /// <summary>
        /// The type that declares the method.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// The method information.
        /// </summary>
        public MethodInfo MethodInfo { get; set; }

        /// <summary>
        /// The minimum confidence necessary to invoke this method.
        /// </summary>
        public float MinConfidence { get; set; } = 0;

        /// <summary>
        /// The maximum confidence allowed to invoke this method.
        /// </summary>
        public float MaxConfidence { get; set; } = 1;

        /// <summary>
        /// Whether partial responses should be validated
        /// </summary>
        public bool ValidatePartial { get; set; } = false;
    }
}
