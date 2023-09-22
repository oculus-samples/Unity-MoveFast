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
using System.Reflection;

namespace Meta.Conduit.Editor
{
    /// <summary>
    /// Wraps an assembly and provides access to Conduit-relevant details.
    /// </summary>
    internal class ConduitAssembly : IConduitAssembly
    {
        /// <summary>
        /// The assembly this class wraps.
        /// </summary>
        private readonly Assembly _assembly;
        
        /// <summary>
        /// Initializes the class with a target assembly.
        /// </summary>
        /// <param name="assembly">The assembly to process.</param>
        public ConduitAssembly(Assembly assembly)
        {
            this._assembly = assembly;
        }
        
        public string FullName => this._assembly.FullName;

        public IEnumerable<Type> GetEnumTypes()
        {
            return this._assembly.GetTypes().Where(p => p.IsEnum);
        }

        public IEnumerable<MethodInfo> GetMethods()
        {
            return this._assembly.GetTypes().SelectMany(type => type.GetMethods());
        }
    }
}
