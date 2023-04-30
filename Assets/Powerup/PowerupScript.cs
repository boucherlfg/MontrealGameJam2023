using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    const float powerupLevelRatio = 0.05f;
    //[HideInInspector]
    public float life = 0;
    [HideInInspector]
    public float moveSpeed = 0;
    [HideInInspector]
    public float damage = 0;
    [HideInInspector]
    public float attackSpeed = 0;
    [HideInInspector]
    public float energy = 0;
    [HideInInspector]
    public float energyRegen = 0;
    [HideInInspector]
    public float switchSpeed = 0;

    public AudioClip powerupSound;
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerScript>();
        if (!player) return;

        Gamestate.Instance.Player.Life *= (1 + life * powerupLevelRatio);
        Gamestate.Instance.Player.MaxLife *= (1 + life * powerupLevelRatio);
        Gamestate.Instance.Player.MoveSpeed *= (1 - moveSpeed * powerupLevelRatio);
        Gamestate.Instance.Player.Damage *= (1 + damage * powerupLevelRatio);
        Gamestate.Instance.Player.AttackSpeed *= (1 - attackSpeed * powerupLevelRatio);
        Gamestate.Instance.Player.Energy += (1 + energy * powerupLevelRatio);
        Gamestate.Instance.Player.MaxEnergy += (1 + energy * powerupLevelRatio);
        Gamestate.Instance.Player.EnergyRegen += (1 + energyRegen * powerupLevelRatio);
        Gamestate.Instance.Player.SwitchSpeed += (1 - switchSpeed * powerupLevelRatio);
        Notify();
        AudioSource.PlayClipAtPoint(powerupSound, transform.position);
        Destroy(gameObject);
    }
    void Notify()
    {
        if (life > 0) Notification.Create("your life increased!");
        if(moveSpeed > 0) Notification.Create("your move speed increased!");
        if (damage > 0) Notification.Create("your damage increased!");
        if (attackSpeed > 0) Notification.Create("your attack speed increased!");
        if (energy > 0) Notification.Create("your energy increased!");
        if (energyRegen > 0) Notification.Create("your energy regeneration increased!");
        if (switchSpeed > 0) Notification.Create("your time power recovery speed increased!");
    }
}
