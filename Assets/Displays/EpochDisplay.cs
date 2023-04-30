using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpochDisplay : MonoBehaviour
{
    private TMPro.TMP_Text label;
    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        label.text = ">Q - " + Gamestate.Instance.Epoch + " - E<";
    }
}
