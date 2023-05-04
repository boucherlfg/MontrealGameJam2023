using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownDisplay : MonoBehaviour
{
    public Slider slider;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        Gamestate.Instance.Player.SwitchCooldown.Changed += UpdateDisplay;
        Gamestate.Instance.Player.SwitchSpeed.Changed += UpdateDisplay;
        UpdateDisplay();
    }

    void OnDestroy()
    {
        Gamestate.Instance.Player.SwitchSpeed.Changed -= UpdateDisplay;
        Gamestate.Instance.Player.SwitchCooldown.Changed -= UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        int value = Mathf.CeilToInt(Gamestate.Instance.Player.SwitchCooldown.Value);
        slider.value = 1 - Gamestate.Instance.Player.SwitchCooldown.Value;
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
