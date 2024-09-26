// Copyright (c) Meta Platforms, Inc. and affiliates.

using Oculus.Interaction;
using Oculus.Interaction.MoveFast;
using Oculus.Interaction.PoseDetection;
using UnityEngine;

public class HandPoseActiveState : MonoBehaviour, IActiveState
{
    [SerializeField]
    IHandReference _hand;

    [SerializeField]
    PoseRecognizer _poseRecognizer;

    [SerializeField]
    TransformRecognizerActiveState _transform;

    [SerializeField]
    ShapeRecognizerActiveState _shape;

    [SerializeField]
    ReferenceActiveState _canHitTarget;

    public bool Active => _canHitTarget && (!_transform || _transform.Active) && (!_shape || _shape.Active);// _poseRecognizer.IsMatch(_hand);
}
