using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [Scene]
    public string mainMenuScene;
    public void MainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(mainMenuScene);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
