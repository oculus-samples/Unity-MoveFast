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
using Facebook.WitAi.Data;
using Facebook.WitAi.Lib;
using Meta.Conduit.Editor;

namespace Facebook.WitAi.Windows
{
    /// <summary>
    /// Validates whether a data type if supported by Wit.
    /// </summary>
    public class WitParameterValidator : IParameterValidator
    {
        /// <summary>
        /// These are the types that we natively support.
        /// </summary>
        private readonly HashSet<Type> builtInTypes = new HashSet<Type>() { typeof(string), typeof(int) };

        /// <summary>
        /// Tests if a parameter type can be supplied directly to a callback method from.
        /// </summary>
        /// <param name="type">The data type.</param>
        /// <returns>True if the parameter type is supported. False otherwise.</returns>
        public bool IsSupportedParameterType(Type type)
        {
            return type.IsEnum || this.builtInTypes.Contains(type) || type == typeof(WitResponseNode) || type == typeof(VoiceSession);
        }
    }
}
