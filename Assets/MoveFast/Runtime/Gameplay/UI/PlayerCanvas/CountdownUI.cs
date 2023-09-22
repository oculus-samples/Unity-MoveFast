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
