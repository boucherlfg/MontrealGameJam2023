using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Rigidbody2D rbody;
    [HideInInspector]
    public Vector2 velocity;
    private float duration = 3;
    public AudioClip hitSound;

    public GameObject bloodEffect;

    public void Update()
    {
        if (Gamestate.Instance.Paused)
        {
            rbody.velocity = Vector2.zero;
            return;
        }
        rbody.velocity = velocity * 20;
        duration -= Time.deltaTime;
        if (duration < 0) Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyScript>();
        if (!enemy) return;

        enemy.lifeCounter--;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
        Destroy(gameObject);
    }
}
