using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDisplay : MonoBehaviour
{
    public TMPro.TMP_Text label;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int value = Mathf.CeilToInt(Gamestate.Instance.Potions);
        label.text = value + "";
    }
}
