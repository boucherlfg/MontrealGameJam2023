using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSwitch : MonoBehaviour
{
    private Music music;
    public GameObject[] environments;
    public Color[] backgrounds;

    public string[] epochString;

    public AudioClip switchSound;
    // Start is called before the first frame update
    void Start()
    {
        Gamestate.Instance.EnvironmentIndex = 0;
        music = FindObjectOfType<Music>();
        music.followEpochs = true;

        for (int i = 0; i < environments.Length; i++)
        {
            if (i == Gamestate.Instance.EnvironmentIndex) continue;
            environments[i].SetActive(false);
        }
        Gamestate.Instance.Epoch = epochString[Gamestate.Instance.EnvironmentIndex];
        Camera.main.backgroundColor = backgrounds[Gamestate.Instance.EnvironmentIndex];
    }
    void OnDestroy()
    {
        music.followEpochs = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused)
        {
            return;
        }
        int switchDirection = 0;
        if (Input.GetKeyUp(KeyCode.Q)) switchDirection = -1;
        else if (Input.GetKeyUp(KeyCode.E)) switchDirection = 1;

        if (switchDirection != 0)
        {
            if (Gamestate.Instance.SwitchCooldown > 0) return;
            StartCoroutine(ChangeTime(switchDirection));
        }

        Gamestate.Instance.SwitchCooldown -= Time.deltaTime;
        if (Gamestate.Instance.SwitchCooldown < 0)
        {
            Gamestate.Instance.SwitchCooldown = 0;
        }
    }

    IEnumerator ChangeTime(int switchDirection)
    {
        AudioSource.PlayClipAtPoint(switchSound, Gamestate.Instance.Position);
        Gamestate.Instance.SwitchCooldown = Gamestate.Instance.Player.SwitchSpeed;
        yield return Fade.DoFade(1, 0.5f);

        environments[Gamestate.Instance.EnvironmentIndex].SetActive(false);
        Gamestate.Instance.EnvironmentIndex += switchDirection;
        if (Gamestate.Instance.EnvironmentIndex < 0) Gamestate.Instance.EnvironmentIndex = environments.Length - 1;
        if (Gamestate.Instance.EnvironmentIndex >= environments.Length) Gamestate.Instance.EnvironmentIndex = 0;

        environments[Gamestate.Instance.EnvironmentIndex].SetActive(true);
        Camera.main.backgroundColor = backgrounds[Gamestate.Instance.EnvironmentIndex];
        Gamestate.Instance.Epoch = epochString[Gamestate.Instance.EnvironmentIndex];
        Notification.Create("welcome to " + Gamestate.Instance.Epoch + "!");
        yield return Fade.DoFade(0, 0.5f);
    }
}
