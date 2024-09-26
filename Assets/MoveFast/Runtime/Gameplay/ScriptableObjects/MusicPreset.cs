// Copyright (c) Meta Platforms, Inc. and affiliates.

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
