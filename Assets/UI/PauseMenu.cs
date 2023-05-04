using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    [Scene]
    public string mainMenuScene;

    void Start()
    {
        Gamestate.Instance.Paused.Changed += Paused_Changed;
    }
    void OnDestroy()
    {
        Gamestate.Instance.Paused.Changed -= Paused_Changed;
    }
    private void Paused_Changed()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            foreach (Transform t in transform)
            {
                t.gameObject.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        Gamestate.Instance.Paused.Value = !Gamestate.Instance.Paused.Value;
    }
    public void Mainmenu()
    {
        Gamestate.Instance.Paused.Value = !Gamestate.Instance.Paused.Value;
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuScene);
    }
}
