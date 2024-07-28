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

using System.Collections;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Opens the pause menu when the user makes the pause hgesture or the app loses focus
    /// </summary>
    public class PauseHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private StringPropertyRef _pauseProperty;
        [SerializeField]
        private ReferenceActiveState _canPause;

        public static bool IsPaused { get; private set; }
        private const OVRInput.Button _start = OVRInput.Button.Start;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);
            OVRManager.InputFocusLost += CheckPause;
        }

        private void OnDestroy()
        {
            OVRManager.InputFocusLost -= PauseGame;
        }

        private void Update()
        {
            if (OVRInput.GetDown(_start) && _canPause)
                TogglePause();
        }

        private void CheckForPause()
        {
            if (_canPause)
            {
                PauseGame();
            }
        }

        private void CheckPause() => CheckForPause();
        private void TogglePause() => SetPaused(!IsPaused);
        private void PauseGame() => SetPaused(true);

        public void SetPaused(bool pause)
        {
            IsPaused = pause;
            _pauseProperty.Value = pause ? "paused" : "";
        }
    }
}
