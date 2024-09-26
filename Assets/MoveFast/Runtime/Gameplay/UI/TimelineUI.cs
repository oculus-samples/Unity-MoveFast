// Copyright (c) Meta Platforms, Inc. and affiliates.

using Oculus.Interaction.MoveFast;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

/// <summary>
/// Updates a slider based on the curent track time
/// </summary>
public class TimelineUI : MonoBehaviour, IUIListElementHandler
{
    [SerializeField]
    Slider _slider;
    [SerializeField]
    private LayoutElement _layoutElement;

    private TimeRange _timeRange = default;

    public PlayableDirector ActiveTrack;

    private void Update()
    {
        double activeTrackTime = ActiveTrack.time;
        _slider.value = Mathf.Clamp01((float)((activeTrackTime - _timeRange.Start) / _timeRange.Duration));
    }

    public void HandleListElement(object element)
    {
        if (element is TimeRange timeRange)
        {
            _timeRange = timeRange;
            _layoutElement.flexibleWidth = (float)timeRange.Duration;
        }
    }
}
