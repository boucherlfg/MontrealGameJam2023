using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyDisplay : MonoBehaviour
{
    public Image icon;
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Gamestate.Instance.Player.IsExhausted.Changed += UpdateDisplay;
        Gamestate.Instance.Player.Energy.Changed += UpdateDisplay;
        Gamestate.Instance.Player.MaxEnergy.Changed += UpdateDisplay;
        UpdateDisplay();
    }

    void OnDestroy()
    {
        Gamestate.Instance.Player.IsExhausted.Changed -= UpdateDisplay;
        Gamestate.Instance.Player.Energy.Changed -= UpdateDisplay;
        Gamestate.Instance.Player.MaxEnergy.Changed -= UpdateDisplay;
    }


    private void UpdateDisplay()
    {
        icon.color = (Gamestate.Instance.Player.IsExhausted.Value ? Color.gray : Color.white);
        slider.value = Gamestate.Instance.Player.Energy.Value / Gamestate.Instance.Player.MaxEnergy.Value;
    }
}
