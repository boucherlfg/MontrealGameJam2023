using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDisplay : MonoBehaviour
{
    public Image dyno;
    public Image scarrab;
    public Image death;
    public Image ufo;

    // Update is called once per frame
    void Update()
    {
        dyno.gameObject.SetActive(Gamestate.Instance.Environments[0].FinalBossIsBeaten);
        scarrab.gameObject.SetActive(Gamestate.Instance.Environments[1].FinalBossIsBeaten);
        death.gameObject.SetActive(Gamestate.Instance.Environments[2].FinalBossIsBeaten);
        ufo.gameObject.SetActive(Gamestate.Instance.Environments[3].FinalBossIsBeaten);
    }
}
