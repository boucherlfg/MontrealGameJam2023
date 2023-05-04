using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGameOnDeath : MonoBehaviour
{
    [Scene]
    public string winScene;
    void OnDestroy()
    {
        if (GetComponent<EnemyScript>().lifeCounter <= 0)
        {
            Music.Instance.StartCoroutine(GoToWinSceneAfterDelay());
        }
    }
    IEnumerator GoToWinSceneAfterDelay()
    {
        yield return Fade.Instance.FadeAsync(1, 3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(winScene);
    }
}
