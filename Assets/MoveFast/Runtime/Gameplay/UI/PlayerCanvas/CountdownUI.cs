// Copyright (c) Meta Platforms, Inc. and affiliates.

using System.Collections;
using TMPro;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Handles the UI that appears before a track starts
    /// </summary>
    public class CountdownUI : ActiveStateObserver, IActiveState
    {
        static readonly WaitForSeconds _oneSecond = new WaitForSeconds(1);

        [SerializeField]
        private GameObject _countdownParent;
        [SerializeField]
        private GameObject _countdownIcon;
        [SerializeField]
        private TextMeshProUGUI _countdownText;
        [SerializeField]
        private int _countdown;
        private bool _countingDown;

        bool IActiveState.Active => _countingDown;

        protected override void HandleActiveStateChanged()
        {
            if (Active) StartCountdown();
        }

        private void StartCountdown()
        {
            StartCoroutine(CountdownRoutine());
            IEnumerator CountdownRoutine()
            {
                _countingDown = true;
                _countdownParent.SetActive(true);
                for (int i = _countdown; i > 0; i--)
                {
                    _countdownText.SetText(i.ToString());
                    yield return _oneSecond;
                }
                _countdownText.SetText("GO");
                yield return _oneSecond;
                _countdownParent.SetActive(false);
                _countingDown = false;
            }
        }
    }
}
