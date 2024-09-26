// Copyright (c) Meta Platforms, Inc. and affiliates.

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Oculus.Interaction.MoveFast
{
    /// <summary>
    /// Handles the volume slider functionality in the pause menu
    /// </summary>
    public class VolumeSliderHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private AudioMixer _audioMixer;
        [SerializeField]
        private AudioTrigger _pressAudio, _releaseAudio;

        private float _defaultVolume = 0;
        private Slider _slider;

        private void Start()
        {
            _slider = GetComponent<Slider>();
            _slider.value = _defaultVolume;
        }

        private void Update() => _audioMixer.SetFloat("MasterVolume", _slider.value);

        public void OnPointerEnter(PointerEventData eventData) => _pressAudio.PlayAudio();
        public void OnPointerExit(PointerEventData eventData) => _releaseAudio.PlayAudio();
    }
}
