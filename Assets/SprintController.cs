using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Inputs.Instance.Sprinted += OnSprint;
    }
    void OnDestroy()
    {
        Inputs.Instance.Sprinted -= OnSprint;
    }

    void OnSprint(bool value)
    {
        if (Gamestate.Instance.Paused.Value)
        {
            return;
        }
        Gamestate.Instance.Player.IsSprinting.Value = !Gamestate.Instance.Player.IsExhausted.Value && value;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            return;
        }
        if (Gamestate.Instance.Player.IsSprinting)
        {
            Gamestate.Instance.Player.Energy.Value -= Time.deltaTime;
            if (Gamestate.Instance.Player.Energy.Value <= 0)
            {
                Gamestate.Instance.Player.Energy.Value = 0;
                Gamestate.Instance.Player.IsSprinting.Value = false;
                Gamestate.Instance.Player.IsExhausted.Value = true;
                return;
            }
        }
        else
        {
            Gamestate.Instance.Player.Energy.Value += Gamestate.Instance.Player.EnergyRegen.Value * Time.deltaTime;
            if (Gamestate.Instance.Player.Energy.Value >= Gamestate.Instance.Player.MaxEnergy.Value)
            {
                Gamestate.Instance.Player.IsExhausted.Value = false;
                Gamestate.Instance.Player.Energy.Value = Gamestate.Instance.Player.MaxEnergy.Value;
            }
        }
    }
}
