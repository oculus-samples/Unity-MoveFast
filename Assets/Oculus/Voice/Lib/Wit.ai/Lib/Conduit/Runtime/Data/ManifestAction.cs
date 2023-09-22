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
using System.Linq;

namespace Meta.Conduit
{
    /// <summary>
    /// An action entry in the manifest.
    /// </summary>
    internal class ManifestAction
    {
        /// <summary>
        /// Called via JSON reflection, need preserver or it will be stripped on compile
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public ManifestAction() { }

        /// <summary>
        /// This is the internal fully qualified name of the method in the codebase.
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// The fully qualified name of the assembly containing the code for the action.
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        /// The name of the action as exposed to the backend.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The parameters used by the action.
        /// </summary>
        public List<ManifestParameter> Parameters { get; set; } = new List<ManifestParameter>();

        /// <summary>
        /// Returns the fully qualified name of the declaring type of the action.
        /// </summary>
        public string DeclaringTypeName => ID.Substring(0, ID.LastIndexOf('.'));

        /// <summary>
        /// Additional names by which the backend can refer to this action.
        /// </summary>
        public List<string> Aliases { get; set; } = new List<string>();

        public override bool Equals(object obj)
        {
            return obj is ManifestAction other && this.Equals(other);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 31 + ID.GetHashCode();
            hash = hash * 31 + Assembly.GetHashCode();
            hash = hash * 31 + Name.GetHashCode();
            hash = hash * 31 + Parameters.GetHashCode();
            hash = hash * 31 + Aliases.GetHashCode();
            return hash;
        }

        private bool Equals(ManifestAction other)
        {
            return this.ID == other.ID && this.Assembly == other.Assembly && this.Name == other.Name && this.Parameters.SequenceEqual(other.Parameters) && this.Aliases.SequenceEqual(other.Aliases);
        }
    }
}
