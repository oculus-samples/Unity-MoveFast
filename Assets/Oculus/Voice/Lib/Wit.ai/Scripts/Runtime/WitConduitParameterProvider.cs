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
using Facebook.WitAi.Data;
using Facebook.WitAi.Lib;
using Meta.Conduit;

namespace Facebook.WitAi
{
    internal class WitConduitParameterProvider : ParameterProvider
    {
        public const string WitResponseNodeReservedName = "@WitResponseNode";
        public const string VoiceSessionReservedName = "@VoiceSession";
        protected override object GetSpecializedParameter(ParameterInfo formalParameter)
        {
            if (formalParameter.ParameterType == typeof(WitResponseNode) && ActualParameters.ContainsKey(WitResponseNodeReservedName))
            {
                return ActualParameters[WitResponseNodeReservedName];
            }
            else if (formalParameter.ParameterType == typeof(VoiceSession) && ActualParameters.ContainsKey(VoiceSessionReservedName))
            {
                return ActualParameters[VoiceSessionReservedName];
            }
            return null;
        }

        protected override bool SupportedSpecializedParameter(ParameterInfo formalParameter)
        {
            return formalParameter.ParameterType == typeof(WitResponseNode) || formalParameter.ParameterType == typeof(VoiceSession);
        }
    }
}
