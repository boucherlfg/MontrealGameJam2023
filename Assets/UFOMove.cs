using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOMove : MonoBehaviour
{
    private Vector2 velocity;
    public Rigidbody2D rbody;
    public float pauseTime = 2;
    private float pauseCounter;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    // Update is called once per frame
    void Update()
    {
        rbody.velocity = Vector2.zero;
        if (Gamestate.Instance.Paused)
        {
            return;
        }
        rbody.velocity = velocity;
        if (!ReachedDestination()) return;

        pauseCounter += Time.deltaTime / pauseTime;
        if (pauseCounter < 1) return;
        pauseCounter = 0;

        var me = GetComponent<EnemyScript>();

        targetPosition = Gamestate.Instance.Player.Position;
        startPosition = transform.position;
        velocity = (targetPosition - startPosition).normalized * me.speed;
    }
    bool ReachedDestination() 
    {
        var myDistance = Vector2.Distance(startPosition, transform.position);
        var targetDistance = Vector2.Distance(startPosition, targetPosition);
        return myDistance >= targetDistance;
    }
}
