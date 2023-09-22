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

namespace Meta.Conduit
{
    /// <summary>
    /// Creates and caches conduit dispatchers.
    /// </summary>
    internal class ConduitDispatcherFactory
    {
        /// <summary>
        /// Dispatcher instance
        /// </summary>
        private static IConduitDispatcher instance;

        /// <summary>
        /// The instance resolver used to find instance objects at runtime.
        /// </summary>
        private readonly IInstanceResolver instanceResolver;

        /// <summary>
        /// The parameter provider used to resolve parameters during dispatching.
        /// </summary>
        private readonly IParameterProvider parameterProvider;

        public ConduitDispatcherFactory(IInstanceResolver instanceResolver, IParameterProvider parameterProvider)
        {
            this.instanceResolver = instanceResolver;
            this.parameterProvider = parameterProvider;
        }
        
        /// <summary>
        /// Returns a Conduit dispatcher instance. The same instance will be reused past the first request.  
        /// </summary>
        /// <returns>A Conduit dispatcher instance</returns>
        public IConduitDispatcher GetDispatcher()
        {
            return instance = instance ??
                              new ConduitDispatcher(new ManifestLoader(), this.instanceResolver,
                                  this.parameterProvider);
        }
    }
}
