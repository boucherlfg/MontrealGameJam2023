using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    [Scene]
    public string winScene;
    // Update is called once per frame
    void Update()
    {
        if (System.Array.TrueForAll(Gamestate.Instance.Environments, env => env.player.Score >= 10))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(winScene);
        }
    }
}
