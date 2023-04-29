using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    public float life = 1;
    public float moveSpeed = 0;
    public float damage = 0;
    public float attackSpeed = 0;
    public float energy = 0;
    public float switchSpeed = 0;

    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerScript>();
        if (!player) return;

        Gamestate.Instance.Player.Life += life;
        Gamestate.Instance.Player.MaxLife += life;
        Gamestate.Instance.Player.MoveSpeed += moveSpeed;
        Gamestate.Instance.Player.Damage += damage;
        Gamestate.Instance.Player.AttackSpeed += attackSpeed;
        Gamestate.Instance.Player.Energy += energy;
        Gamestate.Instance.Player.MaxEnergy += energy;
        Gamestate.Instance.Player.SwitchSpeed += switchSpeed;

        Destroy(gameObject);
    }
}
