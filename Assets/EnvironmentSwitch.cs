using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSwitch : MonoBehaviour
{
    public TimePeriod.TimePeriodData[] timePeriods;
    public AudioClip switchSound;
    // Start is called before the first frame update
    void Start()
    {
        Gamestate.Instance.InGame.Value = true;

        Gamestate.Instance.Environments.Clear();
        for(int i = 0; i < timePeriods.Length; i++)
        {
            var data = timePeriods[i];
            var timePeriod = new TimePeriod();
            timePeriod.Data = data;
            Gamestate.Instance.Environments.Add(timePeriod);
            data.map.SetActive(false);
        }
        Inputs.Instance.Switched += OnSwitch;
        Gamestate.Instance.Environment.Data.map.SetActive(true);
        Music.Instance.Change(Gamestate.Instance.Environment.Data.music);
    }

    private void OnSwitch(int obj)
    {
        if (Gamestate.Instance.Paused.Value)
        {
            return;
        }
        if (Gamestate.Instance.Player.SwitchCooldown.Value > 0) return;
        StartCoroutine(ChangeTime(obj));
    }

    void OnDestroy()
    {
        Inputs.Instance.Switched -= OnSwitch;
        Gamestate.Instance.InGame.Value = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            return;
        }

        Gamestate.Instance.Player.SwitchCooldown.Value -= Gamestate.Instance.Player.SwitchSpeed.Value * Time.deltaTime;
        if (Gamestate.Instance.Player.SwitchCooldown.Value < 0)
        {
            Gamestate.Instance.Player.SwitchCooldown.Value = 0;
        }
    }

    IEnumerator ChangeTime(int switchDirection)
    {
        AudioSource.PlayClipAtPoint(switchSound, Gamestate.Instance.Player.Position.Value);
        Gamestate.Instance.Player.SwitchCooldown.Value = 1;
        yield return Fade.Instance.FadeAsync(1, 0.5f);

        Gamestate.Instance.Environment.Data.map.SetActive(false);
        Gamestate.Instance.MoveIndex(switchDirection);
        Gamestate.Instance.Environment.Data.map.SetActive(true);
        Music.Instance.Change(Gamestate.Instance.Environment.Data.music);
        yield return Fade.Instance.FadeAsync(0, 0.5f);
    }
}
