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
    /// <summary>
    /// Base class for PropertyBehaviour<T>
    /// </summary>
    public abstract class PropertyBehaviour : MonoBehaviour, IProperty
    {
        public event Action WhenChanged = delegate { };

        protected void InvokeWhenChanged()
        {
            WhenChanged();
        }
    }

    /// <summary>
    /// IProperty implementation that holds a value of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PropertyBehaviour<T> : PropertyBehaviour, IProperty<T>
    {
        public virtual T Value { get => _value; set => SetValue(value); }
        private T _value = default(T);

        public void SetValue(T value)
        {
            if (_value == null && value == null) { return; }
            if (_value != null && _value.Equals(value)) { return; }

            _value = value;
            InvokeWhenChanged();
        }

        public override string ToString()
        {
            return _value.ToString();
        }
    }
}
