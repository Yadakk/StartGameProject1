using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ExposedMixerSlider : MonoBehaviour
{
    public AudioMixer Mixer;
    public Slider Slider;
    public string ExposedName;

    private void Start()
    {
        Slider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float value01)
    {
        Mixer.SetFloat(ExposedName, MathfExtensions.LerpAudio(value01));
    }
}
