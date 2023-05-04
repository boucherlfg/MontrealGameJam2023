using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEnemyScript : MonoBehaviour
{
    public AudioClip sound;
    public string message;
    // Start is called before the first frame update
    void Start()
    {
        if (sound) AudioSource.PlayClipAtPoint(sound, transform.position);
        if (message != string.Empty) Notification.Instance.Create(message);
    }

    void OnDestroy()
    {
        if (GetComponent<EnemyScript>().lifeCounter <= 0)
        {
            Gamestate.Instance.Environment.FinalBossIsBeaten.Value = true;
        }
    }
}
