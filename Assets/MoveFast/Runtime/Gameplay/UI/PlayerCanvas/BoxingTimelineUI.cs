// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;
using static Oculus.Interaction.MoveFast.UIList;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Updates the ui timeline that appears above the trainer while the track is playing
    /// </summary>
    public class BoxingTimelineUI : MonoBehaviour, ILifeCycleHandler
    {
        [Header("Slider Components")]
        [SerializeField]
        private UIList _timelineList;
        [SerializeField]
        private GameObject _boxingPrefab, _restPrefab, _cooldownPrefab;
        [SerializeField]
        StringPropertyRef _gameState;

        private void Awake()
        {
            _gameState.WhenChanged += PopulateTimeline;
        }

        private void OnDestroy()
        {
            _gameState.WhenChanged -= PopulateTimeline;
        }

        public void PopulateTimeline()
        {
            // delayed so the track can for sure be playing
            TweenRunner.DelayedCall(0.1f, () =>
            {
                var activeTrack = TrackTimeline.Current?.PlayableDirector;
                if (activeTrack == null) { return; }

                var markerTracks = MarkerTrack.GetTimeRanges(activeTrack);
                markerTracks.RemoveAll(x => !IsDisplayable(x));

                _timelineList.LifeCycleHandler = this;
                _timelineList.SetList(markerTracks);
                _timelineList.ForEachInstance(x => x.GetComponent<TimelineUI>().ActiveTrack = activeTrack);
            });
        }

        GameObject ILifeCycleHandler.Create(object data, Transform parent)
        {
            if (data is TimeRange timeRange)
            {
                var prefab = GetPrefabForType(timeRange.TimelineName);
                return Instantiate(prefab, parent);
            }

            throw new System.Exception($"Can't create instance for {data}");
        }

        void ILifeCycleHandler.Destroy(GameObject value)
        {
            Destroy(value);
        }

        private GameObject GetPrefabForType(string type)
        {
            switch (type)
            {
                case "boxing": return _boxingPrefab;
                case "rest": return _restPrefab;
                case "cooldown": return _cooldownPrefab;
                default: throw new System.Exception($"Can't handle {type}");
            }
        }

        private bool IsDisplayable(TimeRange range)
        {
            switch (range.TimelineName)
            {
                case "boxing":
                case "rest":
                case "cooldown":
                    return true;
                default:
                    return false;
            }
        }
    }
}
