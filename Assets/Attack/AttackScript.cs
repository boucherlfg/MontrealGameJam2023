using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public Rigidbody2D rbody;
    [HideInInspector]
    public Vector2 velocity;
    public float damage;
    private float duration = 3;
    public AudioClip hitSound;
    public const float speed = 20;

    public GameObject bloodEffect;

    public void Update()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            rbody.velocity = Vector2.zero;
            return;
        }
        rbody.velocity = velocity * speed;
        duration -= Time.deltaTime;
        if (duration < 0) Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyScript>();
        if (!enemy) return;

        enemy.lifeCounter -= damage;
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(hitSound, transform.position);
        Destroy(gameObject);
    }


}
