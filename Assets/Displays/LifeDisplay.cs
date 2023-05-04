using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeDisplay : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        Gamestate.Instance.Player.Life.Changed += UpdateDisplay;
        Gamestate.Instance.Player.MaxLife.Changed += UpdateDisplay;
        UpdateDisplay();
    }

    void OnDestroy()
    {
        Gamestate.Instance.Player.Life.Changed -= UpdateDisplay;
        Gamestate.Instance.Player.MaxLife.Changed -= UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        slider.value = Gamestate.Instance.Player.Life.Value / Gamestate.Instance.Player.MaxLife.Value;
    }
}
