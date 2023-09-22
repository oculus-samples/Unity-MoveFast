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
