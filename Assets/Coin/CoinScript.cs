using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public string visibleName;
    private Rigidbody2D body;
    public AudioClip coinSound;
    public GameObject scoreParticle;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerScript>();
        if (!player) return;

        Gamestate.Instance.Player.Score++;
        Notification.Create("You just collected a " + visibleName + "!");
        AudioSource.PlayClipAtPoint(coinSound, transform.position);
        Instantiate(scoreParticle, transform.position, Quaternion.identity);
        Gamestate.Instance.Tutorial[TutorialScript.TutorialType.Score] = true;
        Destroy(gameObject);
    }
}
