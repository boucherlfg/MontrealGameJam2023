using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDisplay : MonoBehaviour
{
    public TMPro.TMP_Text label;
    // Start is called before the first frame update
    void Start()
    {
        Gamestate.Instance.Player.Potions.Changed += UpdateDisplay;
        UpdateDisplay();
    }
    void OnDestroy()
    {
        Gamestate.Instance.Player.Potions.Changed -= UpdateDisplay;
    }

    private void UpdateDisplay()
    {
        int value = Mathf.CeilToInt(Gamestate.Instance.Player.Potions.Value);
        label.text = value + "";
    }
}
