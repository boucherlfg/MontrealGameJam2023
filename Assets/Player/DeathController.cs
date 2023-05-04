using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    [Scene]
    public string gameOverScene;
    public AudioClip deathSound;
    // Start is called before the first frame update
    void Start()
    {

        Gamestate.Instance.Player.Life.Changed += Life_Changed;
    }
    void OnDestroy()
    {

        Gamestate.Instance.Player.Life.Changed -= Life_Changed;
    }
    private void Life_Changed()
    {
        if (Gamestate.Instance.Paused) return;
        if (Gamestate.Instance.Player.Life.Value <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameOverScene);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
