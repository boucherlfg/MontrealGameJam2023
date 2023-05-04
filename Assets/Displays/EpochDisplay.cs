using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpochDisplay : MonoBehaviour
{
    private EnvironmentSwitch environments;
    private TMPro.TMP_Text label;
    // Start is called before the first frame update
    void Start()
    {
        label = GetComponent<TMPro.TMP_Text>();
        Gamestate.Instance.Index.Changed += UpdateTimePeriod;
        UpdateTimePeriod();
        environments = GetComponent<EnvironmentSwitch>();
    }
    void OnDestroy()
    {
        Gamestate.Instance.Index.Changed -= UpdateTimePeriod;
    }

    private void UpdateTimePeriod()
    {
        label.text = "<Q - " + Gamestate.Instance.Environment.Data.visibleName + " - E>";
        Notification.Instance.Create("welcome to " + Gamestate.Instance.Environment.Data.visibleName + "!");
    }
}
