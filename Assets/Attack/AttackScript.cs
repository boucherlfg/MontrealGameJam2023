using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private Rigidbody2D rbody;
    [HideInInspector]
    public Vector2 velocity;
    private float duration = 3;

    IEnumerator Start()
    {
        yield return null;
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = ((Vector3)velocity) * 20;
    }
    public void Update()
    {
        duration -= Time.deltaTime;
        if (duration < 0) Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var enemy = collision.GetComponent<EnemyScript>();
        if (!enemy) return;

        enemy.lifeCounter--;
        Destroy(gameObject);
    }
}
