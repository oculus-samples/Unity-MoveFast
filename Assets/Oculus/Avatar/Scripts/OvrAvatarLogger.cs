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

using UnityEngine;

namespace Oculus.Avatar
{
    public static class AvatarLogger
    {
        public const string LogAvatar = "[Avatars] - ";
        public const string Tab = "    ";

        [System.Diagnostics.Conditional("ENABLE_AVATAR_LOGS"),
            System.Diagnostics.Conditional("ENABLE_AVATAR_LOG_BASIC")]
        public static void Log(string logMsg)
        {
            Debug.Log(LogAvatar + logMsg);
        }

        [System.Diagnostics.Conditional("ENABLE_AVATAR_LOGS"),
            System.Diagnostics.Conditional("ENABLE_AVATAR_LOG_BASIC")]
        public static void Log(string logMsg, Object context)
        {
            Debug.Log(LogAvatar + logMsg , context);
        }

        [System.Diagnostics.Conditional("ENABLE_AVATAR_LOGS"), 
            System.Diagnostics.Conditional("ENABLE_AVATAR_LOG_WARNING")]
        public static void LogWarning(string logMsg)
        {
            Debug.LogWarning(LogAvatar + logMsg);
        }

        [System.Diagnostics.Conditional("ENABLE_AVATAR_LOGS"), 
            System.Diagnostics.Conditional("ENABLE_AVATAR_LOG_ERROR")]
        public static void LogError(string logMsg)
        {
            Debug.LogError(LogAvatar + logMsg);
        }

        [System.Diagnostics.Conditional("ENABLE_AVATAR_LOGS"),
         System.Diagnostics.Conditional("ENABLE_AVATAR_LOG_ERROR")]
        public static void LogError(string logMsg, Object context)
        {
            Debug.LogError(LogAvatar + logMsg, context);
        }
    };
}
