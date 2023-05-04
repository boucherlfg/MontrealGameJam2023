using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public string visibleName;
    public AudioClip coinSound;
    public GameObject scoreParticle;
    // Start is called before the first frame update
    void Start()
    {
        Notification.Instance.Create("a " + visibleName + " has just appeared!");
        Gamestate.Instance.Props[name]++;
    }
    void OnDestroy()
    {
        Gamestate.Instance.Props[name]--;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerScript>();
        if (!player) return;

        Gamestate.Instance.Environment.Score.Value++;
        Notification.Instance.Create("You just collected a " + visibleName + "!");
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
        Instantiate(scoreParticle, transform.position, Quaternion.identity);
        Gamestate.Instance.Environment.Tutorial[TutorialType.Score] = true;
        Destroy(gameObject);
    }
}
