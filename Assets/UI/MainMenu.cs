using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Scene]
    public string gameScene;
    public GameObject howTo;
    void Start()
    {
        Music.Instance.Change(Music.Instance.menuMusic);
    }
    public void Play()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(gameScene);
    }
    public void Howto()
    {
        howTo.SetActive(true);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
