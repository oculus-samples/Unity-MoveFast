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
using System.IO;
using UnityEngine;

namespace Facebook.WitAi.Utilities
{
    public static class IOUtility
    {
        // Log error
        private static void LogError(string error)
        {
            Debug.LogError($"IO Utility - {error}");
        }

        /// <summary>
        /// Creates a directory recursively if desired and returns true if successful
        /// </summary>
        /// <param name="directoryPath">The directory to be created</param>
        /// <param name="recursively">Will traverse parent directories if needed</param>
        /// <returns>Returns true if the directory exists</returns>
        public static bool CreateDirectory(string directoryPath, bool recursively = true)
        {
            // Null
            if (string.IsNullOrEmpty(directoryPath))
            {
                return false;
            }

            // Already exists
            if (Directory.Exists(directoryPath))
            {
                return true;
            }

            // Check parent
            if (recursively)
            {
                string parentDirectoryPath = Path.GetDirectoryName(directoryPath);
                if (!string.IsNullOrEmpty(parentDirectoryPath) && !CreateDirectory(parentDirectoryPath, true))
                {
                    return false;
                }
            }

            try
            {
                Directory.CreateDirectory(directoryPath);
            }
            catch (Exception e)
            {
                LogError($"Create Directory Exception\nDirectory Path: {directoryPath}\n{e}");
                return false;
            }

            // Successfully created
            return true;
        }

        /// <summary>
        /// Deletes a directory and returns true if the directory no longer exists
        /// </summary>
        /// <param name="directoryPath">The directory to be created</param>
        /// <param name="forceIfFilled">Whether to force a deletion if the directory contains contents</param>
        /// <returns>Returns true if the directory does not exist</returns>
        public static bool DeleteDirectory(string directoryPath, bool forceIfFilled = true)
        {
            // Already gone
            if (!Directory.Exists(directoryPath))
            {
                return true;
            }

            try
            {
                Directory.Delete(directoryPath, forceIfFilled);
            }
            catch (Exception e)
            {
                LogError($"Delete Directory Exception\nDirectory Path: {directoryPath}\n{e}");
                return false;
            }

            // Successfully deleted
            return true;
        }
    }
}
