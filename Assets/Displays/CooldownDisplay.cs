using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownDisplay : MonoBehaviour
{
    private string labelText;
    public Slider slider;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int value = Mathf.CeilToInt(Gamestate.Instance.SwitchCooldown);
        slider.value = 1 - Gamestate.Instance.SwitchCooldown;
        if (slider.value < 0.99)
        {
            image.color = Color.grey;
        }
        else
        {
            image.color = Color.white;
        }
    }
}
