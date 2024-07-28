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

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Holds data about the song for displaying on the UI
    /// </summary>
    [CreateAssetMenu(fileName = "MusicPreset", menuName = "ScriptableObject/MusicPreset")]
    public class MusicPreset : ScriptableObject
    {
        [SerializeField]
        private Sprite _trackCoverArt;
        public Sprite TrackCoverArt => _trackCoverArt;

        [SerializeField]
        private string _trackName;
        public string TrackName => _trackName;

        [SerializeField]
        private string _trackArtist;
        public string TrackArtist => _trackArtist;

        [SerializeField]
        private string _trackGenre;
        public string TrackGenre => _trackGenre;

        [SerializeField]
        private string _trackBPM;
        public string TrackBPM => _trackBPM;

        [SerializeField]
        private Sprite _trackDifficulty;
        public Sprite TrackDifficulty => _trackDifficulty;
    }
}
