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
using System.Reflection;

namespace Meta.Conduit
{
    /// <summary>
    /// Resolves parameters for invoking callbacks.
    /// </summary>
    internal interface IParameterProvider
    {
        /// <summary>
        /// Must be called after all parameters have been obtained and mapped but before any are read.
        /// </summary>
        void Populate(Dictionary<string, object> actualParameters, Dictionary<string, string> parameterToRoleMap);

        /// <summary>
        /// Returns true if a parameter with the specified name can be provided. 
        /// </summary>
        /// <param name="parameter">The name of the parameter.</param>
        /// <returns>True if a parameter with the specified name can be provided.</returns>
        bool ContainsParameter(ParameterInfo parameter);

        /// <summary>
        /// Returns the actual value for a formal parameter.
        /// </summary>
        /// <param name="formalParameter">The parameter info.</param>
        /// <returns>The actual parameter value.</returns>
        object GetParameterValue(ParameterInfo formalParameter);
    }
}
