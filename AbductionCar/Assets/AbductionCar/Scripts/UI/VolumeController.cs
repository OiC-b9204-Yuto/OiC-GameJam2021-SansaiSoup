using AbductionCar.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AbductionCar.UI
{

    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private AudioManager.AudioGroup groupName;
        [SerializeField] private Slider slider;
        [SerializeField] private float volumeMin;
        [SerializeField] private float volumeMax;
        [SerializeField] private AudioClip sound;
        [SerializeField, Range(0.0f, 1.0f)] private float volumeScale = 1.0f;
        private const float SoundCooldownTime = 0.1f;
        private float time = SoundCooldownTime;

        private void Awake()
        {
            time = SoundCooldownTime;
            slider.minValue = volumeMin;
            slider.maxValue = volumeMax;
            slider.SetValueWithoutNotify(AudioManager.Instance.GetVolume(groupName));
            slider.onValueChanged.AddListener(delegate { ValueChanged(); });
        }

        private void Update()
        {
            if (time >= 0.0f)
            {
                time -= Time.unscaledDeltaTime;
            }
        }

        public void SliderReset()
        {
            slider.SetValueWithoutNotify(AudioManager.Instance.GetVolume(groupName));
        }

        void ValueChanged()
        {
            if (slider.value == slider.minValue)
            {
                AudioManager.Instance.SetVolume(groupName, -80.0f);
            }
            else
            {
                AudioManager.Instance.SetVolume(groupName, slider.value);
            }
            if(sound)
            {
                if (time <= 0.0f)
                {
                    AudioManager.Instance.SE.PlayOneShot(sound, volumeScale);
                    time += SoundCooldownTime;
                }
            }
        }
    }
}