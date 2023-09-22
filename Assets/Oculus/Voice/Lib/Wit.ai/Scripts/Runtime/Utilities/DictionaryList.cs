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

namespace Facebook.WitAi.Utilities
{
    public class DictionaryList<T, U>
    {
        private Dictionary<T, List<U>> dictionary = new Dictionary<T, List<U>>();

        public void Add(T key, U value)
        {
            if (!TryGetValue(key, out var values))
            {
                dictionary[key] = values;
            }
            values.Add(value);
        }

        public void RemoveAt(T key, int index)
        {
            if (TryGetValue(key, out var values)) values.RemoveAt(index);
        }

        public void Remove(T key, U value)
        {
            if (TryGetValue(key, out var values)) values.Remove(value);
        }

        #region Getters
        public List<U> this[T key]
        {
            get
            {
                List<U> values;
                if (!TryGetValue(key, out values))
                {
                    values = new List<U>();
                    dictionary[key] = values;
                }
                return values;
            }
            set => dictionary[key] = value;
        }

        public bool TryGetValue(T key, out List<U> values)
        {
            if (!dictionary.TryGetValue(key, out values))
            {
                values = new List<U>();
                return false;
            }

            return true;
        }
        #endregion
    }
}
