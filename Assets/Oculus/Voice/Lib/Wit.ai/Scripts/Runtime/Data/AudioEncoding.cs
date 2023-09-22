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

namespace Facebook.WitAi.Data
{
    [Serializable]
    public class AudioEncoding
    {
        public enum Endian
        {
            Big,
            Little
        }

        /// <summary>
        /// The expected encoding of the mic pcm data
        /// </summary>
        public string encoding = "signed-integer";

        /// <summary>
        /// The number of bits per sample
        /// </summary>
        public int bits = 16;

        /// <summary>
        /// The sample rate used to capture audio
        /// </summary>
        public int samplerate = 16000;

        /// <summary>
        /// The endianess of the data
        /// </summary>
        public Endian endian = Endian.Little;

        public override string ToString()
        {
            return $"audio/raw;bits={bits};rate={samplerate / 1000}k;encoding={encoding};endian={endian.ToString().ToLower()}";
        }
    }
}
