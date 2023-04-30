using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject howTo;
    [Scene]
    public string mainMenuScene;

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused)
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
        Gamestate.Instance.Paused = !Gamestate.Instance.Paused;
    }
    public void Howto()
    {
        howTo.SetActive(true);
    }
    public void Mainmenu()
    {
        Gamestate.Instance.Paused = !Gamestate.Instance.Paused;
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuScene);
    }
}
