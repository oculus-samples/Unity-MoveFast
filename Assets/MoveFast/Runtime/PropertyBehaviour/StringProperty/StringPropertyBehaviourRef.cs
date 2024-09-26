// Copyright (c) Meta Platforms, Inc. and affiliates.

using System;
using UnityEngine;

namespace Oculus.Interaction.MoveFast
{
    public class StringPropertyBehaviourRef : PropertyBehaviourRef, IProperty<string>
    {
        public string Value
        {
            get => Property.ToString();
            set
            {
                SetPropertyWithString(Property, value);
            }
        }

        public static void SetPropertyWithString(IProperty property, string value)
        {
            switch (property)
            {
                case IProperty<float> floatProp:
                    try
                    {
                        if (float.TryParse(value, out float result))
                            floatProp.Value = result;
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }
                    break;
                case IProperty<int> intProp:
                    intProp.Value = int.Parse(value);
                    break;
                case IProperty<string> stringProp:
                    stringProp.Value = value;
                    break;
                default:
                    throw new System.Exception();
            }
        }
    }
}
