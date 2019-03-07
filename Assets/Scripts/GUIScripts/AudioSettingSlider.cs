using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSettingSlider : MonoBehaviour {
    public Text percentText;
    public Slider audioPowerGauge;
    public AudioMixer audioMixer;
    public string mixerGroupParameterName;

    public void SetPower(float power)
    {
        percentText.text = (int)(power*100)+ "%";
        audioMixer.SetFloat(mixerGroupParameterName, Mathf.Log(power) * 20);
    }
}
