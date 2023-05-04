using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private Rigidbody2D body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var my = GetComponent<EnemyScript>();
        body.velocity = Vector2.zero;
        if (Gamestate.Instance.Paused.Value) return;


        var direction = GetAvoidVector() + GetChaseVector();
        var speed = Gamestate.Instance.Player.IsSprinting ? my.speed * 0.5f : my.speed;
        body.velocity = direction.normalized * speed;
    }
    Vector2 GetAvoidVector()
    {
        Vector2 direction = Vector2.zero;
        foreach (Transform t in transform.parent)
        {
            if (t == transform) continue;
            var comp = t.GetComponent<EnemyScript>();
            if (!comp) continue;

            Vector2 tempDir = comp.transform.position - transform.position;
            var avoidanceFactor = transform.localScale.x;
            var diffDist = avoidanceFactor - tempDir.magnitude;
            if (diffDist > 0)
            {
                direction -= tempDir.normalized * (diffDist / avoidanceFactor) * avoidanceFactor;
            }
        }
        return direction;
    }
    Vector2 GetChaseVector()
    {
        var distVect = (Gamestate.Instance.Player.Position.Value - (Vector2)transform.position);
        return distVect.normalized;
    }
}
