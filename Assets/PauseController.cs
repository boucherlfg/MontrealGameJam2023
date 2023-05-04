using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Inputs.Instance.Paused += Instance_Paused;
    }

    void OnDestroy()
    {
        Inputs.Instance.Paused -= Instance_Paused;
    }
    private void Instance_Paused()
    {
        Gamestate.Instance.Paused.Value = !Gamestate.Instance.Paused.Value;
    }
}
