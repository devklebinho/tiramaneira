using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ChangeAudioValue : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void ChangeMasterValue(Slider slider)
    {
        switch (slider.value)
        {
            case 0:
                audioMixer.SetFloat("MasterVolume", -80f);
                break;
            case 1:
                audioMixer.SetFloat("MasterVolume", -40f);
                break;
            case 2:
                audioMixer.SetFloat("MasterVolume", -20f);
                break;
            case 3:
                audioMixer.SetFloat("MasterVolume", -10f);
                break;
            case 4:
                audioMixer.SetFloat("MasterVolume", 0f);
                break;
            case 5:
                audioMixer.SetFloat("MasterVolume", 10f);
                break;
        }
    }
}