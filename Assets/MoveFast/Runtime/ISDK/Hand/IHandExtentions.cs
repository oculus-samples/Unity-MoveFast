// Copyright (c) Meta Platforms, Inc. and affiliates.

using Oculus.Interaction;
using Oculus.Interaction.ComprehensiveSample;
using Oculus.Interaction.Input;
using Oculus.Interaction.MoveFast;
using UnityEngine;

public static class IHandExtention
{
    public static bool GetCenterEyePose(this IHand hand, out Pose pose)
    {
        pose = Camera.main.transform.GetPose();
        return true;
    }

    public static bool TryGetAspect<TComponent>(this IHand hand, out TComponent foundComponent) where TComponent : class
    {
        if (hand is IHandReference reference)
        {
            hand = reference.Hand;
        }

        if (hand is MonoBehaviour mono)
        {
            if (mono.TryGetComponent(out Aspect aspect) && aspect.TryGetAspect(out foundComponent))
            {
                return true;
            }
            else if (mono is CompoundHandRef compound && compound.Hands != null)
            {
                for (int i = 0; i < compound.Hands.Count; i++)
                {
                    var cHand = compound.Hands[i];
                    if (cHand.TryGetAspect(out foundComponent))
                    {
                        return true;
                    }
                }
            }
        }

        foundComponent = null;
        return false;
    }

    public static bool IsCenterEyePoseValid() { return true; }
    public static Transform TrackingToWorldSpace() { return null; }

}
