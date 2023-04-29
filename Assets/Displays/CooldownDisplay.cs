using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownDisplay : MonoBehaviour
{
    private string labelText;
    private TMPro.TMP_Text label;
    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<TMPro.TMP_Text>();
        labelText = label.text;
    }

    // Update is called once per frame
    void Update()
    {
        int value = Mathf.CeilToInt(Gamestate.Instance.SwitchCooldown);
        label.text = labelText + value;
    }
}
