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

using UnityEngine;
using UnityEngine.Serialization;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Provides a method to spawn and modify an object
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [SerializeField, FormerlySerializedAs("prefab")]
        GameObject _prefab;

        ISpawnerModifier[] _modifiers;

        private void OnEnable()
        {
            _modifiers = GetComponents<ISpawnerModifier>();
        }

        public void Spawn()
        {
            var instance = Instantiate(_prefab, transform.position, transform.rotation); //TODO pooling
            instance.SetActive(true);
            for (int i = 0; i < _modifiers.Length; i++)
            {
                _modifiers[i].Modify(instance);
            }
        }
    }

    interface ISpawnerModifier
    {
        void Modify(GameObject instance);
    }
}
