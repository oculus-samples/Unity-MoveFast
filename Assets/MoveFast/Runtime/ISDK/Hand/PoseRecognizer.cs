// Copyright (c) Meta Platforms, Inc. and affiliates.

using Oculus.Interaction.Input;
using Oculus.Interaction.PoseDetection;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Combines the functionality of Transform Recogniser and Shape Recogniser
    /// </summary>
    [System.Serializable]
    public class PoseRecognizer : ISerializationCallbackReceiver
    {
        [SerializeField]
        private TransformFeatureConfigList _transformFeatureConfigs;

        [SerializeField]
        private TransformConfig _transformConfig;

        [SerializeField, Optional]
        private ShapeRecognizer _handShape;

        public bool IsMatch(IHand hand)
        {
            bool transformOK = true;
            if (hand.TryGetAspect(out TransformFeatureStateProvider orientationRecognizer))
            {
                transformOK = orientationRecognizer.IsMatch(_transformConfig, _transformFeatureConfigs);
            }

            bool shapeOK = true;
            if (_handShape && hand.TryGetAspect(out IFingerFeatureStateProvider shapeRecognizer))
            {
                shapeOK = shapeRecognizer.IsMatch(_handShape);
            }

            return transformOK && shapeOK;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (_transformConfig != null)
            {
                _transformConfig.InstanceId = GetHashCode();
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize() { }
    }

    public static class GestureRecognizerExtensions
    {
        private static bool registered;

        public static bool IsMatch(this IFingerFeatureStateProvider handState, ShapeRecognizer handShape)
        {
            return handState.IsMatch(HandFinger.Index, handShape.IndexFeatureConfigs) &&
                handState.IsMatch(HandFinger.Middle, handShape.MiddleFeatureConfigs) &&
                handState.IsMatch(HandFinger.Ring, handShape.RingFeatureConfigs) &&
                handState.IsMatch(HandFinger.Pinky, handShape.PinkyFeatureConfigs) &&
                handState.IsMatch(HandFinger.Thumb, handShape.ThumbFeatureConfigs);
        }

        public static bool IsMatch(this IFingerFeatureStateProvider handState, HandFinger finger, IReadOnlyList<ShapeRecognizer.FingerFeatureConfig> configs)
        {
            for (int i = 0; i < configs.Count; i++)
            {
                if (!handState.IsStateActive(finger, configs[i].Feature, configs[i].Mode, configs[i].State))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsMatch(this TransformFeatureStateProvider orientationRecognizer, TransformConfig transformConfig, TransformFeatureConfigList transformFeatureConfigs)
        {
            if (!registered)
            {
                registered = true;
                orientationRecognizer.RegisterConfig(transformConfig);
            }

            var configs = transformFeatureConfigs.Values;

            foreach (var config in configs)
            {
                orientationRecognizer.GetCurrentState(transformConfig, config.Feature, out _); //copied from TransformRecognizerActiveState, required to prewarm?

                if (!orientationRecognizer.IsStateActive(transformConfig, config.Feature, config.Mode, config.State))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
