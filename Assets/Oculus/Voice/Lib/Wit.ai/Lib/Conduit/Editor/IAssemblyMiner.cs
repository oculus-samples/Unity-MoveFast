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

namespace Meta.Conduit.Editor
{
    internal interface IAssemblyMiner
    {
        /// <summary>
        /// Initializes the miner for a new extraction and resets statistics.
        /// Must be called before extracting entities or actions for a new extraction operation.
        /// Note: Call this only once when making multiple calls to ExtractEntities and ExtractActions from different
        /// assemblies that are part of the same manifest.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Stores the frequency of method signatures.
        /// Key is signatures in the form: [ReturnTypeId]-[TypeId]:[FrequencyOfType],[TypeId]:[FrequencyOfType].
        /// Value is the number of times this signature was encountered in the last extraction process.
        /// </summary>
        Dictionary<string, int> SignatureFrequency { get; }

        /// <summary>
        /// Stores the frequency of method signatures that were incompatible.
        /// Key is signatures in the form: [#][ReturnTypeId]![TypeId]:[FrequencyOfType],[TypeId]:[FrequencyOfType].
        /// The # at the beginning indicates a static method.
        /// Value is the number of times this signature was encountered in the last extraction process.
        /// </summary>
        Dictionary<string, int> IncompatibleSignatureFrequency { get; }

        /// <summary>
        /// Extracts all entities from the assembly. Entities represent the types used as parameters (such as Enums) of
        /// our methods.
        /// </summary>
        /// <param name="assembly">The assembly to process.</param>
        /// <returns>The list of entities extracted.</returns>
        List<ManifestEntity> ExtractEntities(IConduitAssembly assembly);

        /// <summary>
        /// This method extracts all the marked actions (methods) in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to process.</param>
        /// <returns>List of actions extracted.</returns>
        List<ManifestAction> ExtractActions(IConduitAssembly assembly);
    }
}
