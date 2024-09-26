// Copyright (c) Meta Platforms, Inc. and affiliates.

using System.Collections.Generic;
using UnityEngine;

public class Aspect : MonoBehaviour
{
    [SerializeField]
    private List<Component> _aspects;

    public bool TryGetAspect<TComponent>(out TComponent foundComponent) where TComponent : class
    {
        foundComponent = _aspects.Find(x => x != null && x.GetType() == typeof(TComponent)) as TComponent;
        return foundComponent != null;
    }
}
