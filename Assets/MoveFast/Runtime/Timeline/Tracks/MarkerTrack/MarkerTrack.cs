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

using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Provides methods for authoring time ranges inside a Timeline
    /// </summary>
    public class MarkerTrack : PlayableBehaviour
    {
        public static TimeRange GetStartTime(TimelineAsset timeline, string name)
        {
            foreach (var track in timeline.GetOutputTracks())
            {
                if (!IsValid(track)) continue;

                foreach (var clip in track.GetClips())
                {
                    if (clip.asset is MarkerTrackAsset && clip.displayName == name)
                    {
                        return new TimeRange(clip.start, clip.end, clip.displayName);
                    }
                }
            }
            throw new System.Exception();
        }

        public static List<TimeRange> GetTimeRanges(PlayableDirector director) => GetTimeRanges(director.playableAsset as TimelineAsset);

        public static List<TimeRange> GetTimeRanges(TimelineAsset timeline)
        {
            var result = new List<TimeRange>();
            foreach (var track in timeline.GetOutputTracks())
            {
                if (!IsValid(track)) continue;

                foreach (var clip in track.GetClips())
                {
                    if (clip.asset is MarkerTrackAsset)
                    {
                        result.Add(new TimeRange(clip.start, clip.end, clip.displayName));
                    }
                }
            }
            result.Sort((x, y) => x.Start.CompareTo(y.Start));
            return result;
        }

        private static bool IsValid(TrackAsset track)
        {
            return track.hasClips && track is PlayableTrack;
        }
    }

    public struct TimeRange
    {
        public double Start;
        public double End;
        public string TimelineName;
        public double Duration => End - Start;

        public TimeRange(double start, double end, string name)
        {
            Start = start;
            End = end;
            TimelineName = name;
        }
    }
}
