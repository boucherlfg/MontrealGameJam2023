using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
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
        if (Gamestate.Instance.Paused)
        {
            body.velocity = Vector2.zero;
            return;
        }
        Move();
    }
    private void Move()
    {
        var speed = !Gamestate.Instance.Player.IsSprinting ? Gamestate.Instance.Player.MoveSpeed.Value : Gamestate.Instance.Player.MoveSpeed.Value * 2;
        var move = Inputs.Instance.Move;

        body.velocity = move.normalized * speed;
        Gamestate.Instance.Player.Position.Value = transform.position;
    }
}
