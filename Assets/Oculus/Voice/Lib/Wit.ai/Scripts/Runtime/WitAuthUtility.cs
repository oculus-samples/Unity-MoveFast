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

using Facebook.WitAi.Data.Configuration;
#if UNITY_EDITOR

#endif

namespace Facebook.WitAi
{
    public class WitAuthUtility
    {
        private static string serverToken;
        public static ITokenValidationProvider tokenValidator = new DefaultTokenValidatorProvider();

        public static bool IsServerTokenValid()
        {
            return tokenValidator.IsServerTokenValid(ServerToken);
        }

        public static bool IsServerTokenValid(string token)
        {
            return tokenValidator.IsServerTokenValid(token);
        }

        public static string GetAppServerToken(WitConfiguration configuration,
            string defaultValue = "")
        {
            return GetAppServerToken(configuration?.application?.id, defaultValue);
        }

        public static string GetAppServerToken(string appId, string defaultServerToken = "")
        {
#if UNITY_EDITOR
            return WitSettingsUtility.GetServerToken(appId, defaultServerToken);
#else
        return "";
#endif
        }

        public static string GetAppId(string serverToken, string defaultAppID = "")
        {
#if UNITY_EDITOR
            return WitSettingsUtility.GetServerTokenAppID(serverToken, defaultAppID);
#else
        return "";
#endif
        }

        public static void SetAppServerToken(string appId, string token)
        {
#if UNITY_EDITOR
            WitSettingsUtility.SetServerToken(appId, token);
#endif
        }

        public const string SERVER_TOKEN_ID = "SharedServerToken";
        public static string ServerToken
        {
#if UNITY_EDITOR
            get
            {
                if (null == serverToken)
                {
                    serverToken = WitSettingsUtility.GetServerToken(SERVER_TOKEN_ID);
                }
                return serverToken;
            }
            set
            {
                serverToken = value;
                WitSettingsUtility.SetServerToken(SERVER_TOKEN_ID, serverToken);
            }
#else
        get => "";
#endif
        }

        public class DefaultTokenValidatorProvider : ITokenValidationProvider
        {
            public bool IsTokenValid(string appId, string token)
            {
                return IsServerTokenValid(token);
            }

            public bool IsServerTokenValid(string serverToken)
            {
                return null != serverToken && serverToken.Length == 32;
            }
        }

        public interface ITokenValidationProvider
        {
            bool IsTokenValid(string appId, string token);
            bool IsServerTokenValid(string serverToken);
        }
    }
}
