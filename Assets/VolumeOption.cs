using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeOption : MonoBehaviour
{
    private Slider slider;
    void OnEnable()
    {
        slider = GetComponentInChildren<Slider>();
        slider.value = AudioListener.volume;
    }
    public void ValueChanged(float value)
    {
        AudioListener.volume = value;
    }
}
