using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    public AudioClip potionSound;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<PlayerScript>()) return;
        Gamestate.Instance.Potions += 1;
        Notification.Create("you just collected a health potion!");
        AudioSource.PlayClipAtPoint(potionSound, transform.position);
        Gamestate.Instance.Tutorial[TutorialScript.TutorialType.Potion] = true;
        Destroy(gameObject);
    }
}
