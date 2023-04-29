using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSwitch : MonoBehaviour
{
    public GameObject[] environments;
    public Color[] backgrounds;

    public string[] epochString;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < environments.Length; i++)
        {
            if (i == Gamestate.Instance.Index) continue;
            environments[i].SetActive(false);
        }
        Gamestate.Instance.Epoch = epochString[Gamestate.Instance.Index];
        Camera.main.backgroundColor = backgrounds[Gamestate.Instance.Index];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (Gamestate.Instance.SwitchCooldown > 0) return;
            Gamestate.Instance.SwitchCooldown = Gamestate.Instance.Player.SwitchSpeed;
         
            environments[Gamestate.Instance.Index].SetActive(false);

            Gamestate.Instance.Index++;
            if (Gamestate.Instance.Index >= environments.Length) Gamestate.Instance.Index = 0;
            
            environments[Gamestate.Instance.Index].SetActive(true);
            Camera.main.backgroundColor = backgrounds[Gamestate.Instance.Index];
            Gamestate.Instance.Epoch = epochString[Gamestate.Instance.Index];
        }

        Gamestate.Instance.SwitchCooldown -= Time.deltaTime;
        if (Gamestate.Instance.SwitchCooldown < 0)
        {
            Gamestate.Instance.SwitchCooldown = 0;
        }
    }
}
