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
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Used to save persistent data. Wraps player prefs in something observable
    /// </summary>
    public static class Store
    {
        public static event Action WhenChanged;

        public static int GetInt(string key)
        {
            if (!HasKey(key)) return 0;

            var str = GetString(key);
            int.TryParse(str, out int result);
            return result;
        }

        public static void SetInt(string key, int value) => SetString(key, value.ToString());

        public static float GetFloat(string key)
        {
            if (!HasKey(key)) return 0;

            var str = GetString(key);
            float.TryParse(str, out float result);
            return result;
        }

        public static void SetFloat(string key, float value) => SetString(key, value.ToString("N1"));

        public static void SetMaxFloat(string key, float value) => SetFloat(key, Mathf.Max(value, GetFloat(key)));

        public static void SetMaxInt(string key, int value) => SetInt(key, Math.Max(value, GetInt(key)));

        public static int Increment(string key)
        {
            int value = GetInt(key) + 1;
            SetInt(key, value);
            return value;
        }

        public static bool HasKey(string key) => PlayerPrefs.HasKey(key);

        public static void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            WhenChanged?.Invoke();
        }

        public static string GetString(string key) => PlayerPrefs.GetString(key);

        public static void DeleteKey(string key) => PlayerPrefs.DeleteKey(key);
    }
}
