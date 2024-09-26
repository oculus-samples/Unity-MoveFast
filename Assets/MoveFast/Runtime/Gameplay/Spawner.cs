// Copyright (c) Meta Platforms, Inc. and affiliates.

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
