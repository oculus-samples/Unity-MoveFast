// Copyright (c) Meta Platforms, Inc. and affiliates.

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
