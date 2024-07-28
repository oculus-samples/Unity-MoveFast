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
