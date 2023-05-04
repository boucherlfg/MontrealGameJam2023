using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossTime : MonoBehaviour
{
    public TimePeriod.TimePeriodData finalBossLevel;
    // Start is called before the first frame update
    void Start()
    {
        var timePeriod = new TimePeriod();
        timePeriod.Data = finalBossLevel;
        Gamestate.Instance.FinalBossLevel = timePeriod;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused) return;
        var allBossBeaten = Gamestate.Instance.Environments.TrueForAll(environment => environment.FinalBossIsBeaten);
        if (allBossBeaten && !Gamestate.Instance.InFinalLevel.Value)
        {
            Gamestate.Instance.InFinalLevel.Value = true;
            StartCoroutine(GetComponent<EnvironmentSwitch>().GoTo(-1));
        }
    }
}
