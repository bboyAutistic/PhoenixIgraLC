using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsSlider : MonoBehaviour
{
 
    public AudioMixer audioMixer;
    float sliderValue;


    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

}

