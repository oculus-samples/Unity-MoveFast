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
    /// Adds fade in and fade out methods to AudioTrigger
    /// </summary>
    public static class AudioTriggerExtensions
    {
        public static void PlayAudio(this AudioTrigger audioTrigger, float fadeIn = -1)
        {
            AudioSource _audioSource = audioTrigger.GetComponent<AudioSource>();

            float startVolume = _audioSource.volume;
            audioTrigger.PlayAudio();
            float endVolume = _audioSource.volume;

            if (fadeIn > 0)
            {
                _audioSource.volume = startVolume;

                audioTrigger.StopAllCoroutines();
                audioTrigger.StartCoroutine(FadeRoutine());
                IEnumerator FadeRoutine()
                {
                    float diff = endVolume - startVolume;
                    while (_audioSource.volume < endVolume)
                    {
                        _audioSource.volume += diff * Time.deltaTime / fadeIn;
                        yield return null;
                    }
                    _audioSource.volume = endVolume;
                }
            }
        }

        public static void StopAudio(this AudioTrigger audioTrigger, float fadeOut = -1)
        {
            AudioSource audioSource = audioTrigger.GetComponent<AudioSource>();
            if (fadeOut <= 0)
            {
                audioSource.Stop();
            }
            else
            {
                audioTrigger.StopAllCoroutines();
                audioTrigger.StartCoroutine(FadeRoutine());
                IEnumerator FadeRoutine()
                {
                    float diff = audioSource.volume;
                    while (audioSource.volume > 0)
                    {
                        audioSource.volume -= diff * Time.deltaTime / fadeOut;
                        yield return null;
                    }
                    audioSource.Stop();
                }
            }
        }
    }
}
