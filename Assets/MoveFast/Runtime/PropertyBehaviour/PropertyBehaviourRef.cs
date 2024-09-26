// Copyright (c) Meta Platforms, Inc. and affiliates.

using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Proxy for an IProperty, useful for cross prefab references
    /// </summary>
    public class PropertyBehaviourRef : MonoBehaviour, IProperty
    {
        [SerializeField, Interface(typeof(IProperty))]
        MonoBehaviour _property;
        public IProperty Property;

        public event Action WhenChanged
        {
            add => Property.WhenChanged += value;
            remove => Property.WhenChanged -= value;
        }

        protected virtual void Awake()
        {
            Property = _property as IProperty;
        }

        protected virtual void Start()
        {
            Assert.IsNotNull(Property);
        }

        public override string ToString()
        {
            return Property.ToString();
        }

        public void InjectProperty(IProperty p)
        {
            Property = p;
            _property = p as MonoBehaviour;
        }
    }
}
