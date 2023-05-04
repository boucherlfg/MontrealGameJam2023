using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    public AudioClip potionSound;

    void Start()
    {
        Notification.Instance.Create("a potion has just appeared!");
        Gamestate.Instance.Props[name]++;
    }

    void OnDestroy()
    {
        Gamestate.Instance.Props[name]--;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayerScript>()) return;
        Gamestate.Instance.Player.Potions.Value += 1;
        Notification.Instance.Create("you just collected a health potion!");
        AudioSource.PlayClipAtPoint(potionSound, transform.position);
        Gamestate.Instance.Environment.Tutorial[TutorialType.Potion] = true;
        Destroy(gameObject);
    }
}
