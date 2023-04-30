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

        Gamestate.Instance.Player.Life += Gamestate.Instance.Player.MaxLife * life * powerupLevelRatio * 3;
        Gamestate.Instance.Player.MaxLife += Gamestate.Instance.Player.MaxLife * life * powerupLevelRatio * 3;
        Gamestate.Instance.Player.MoveSpeed += Gamestate.Instance.Player.MoveSpeed * moveSpeed * powerupLevelRatio * 0.8f;
        Gamestate.Instance.Player.Damage += Gamestate.Instance.Player.Damage * damage * powerupLevelRatio * 3;
        Gamestate.Instance.Player.AttackSpeed += Gamestate.Instance.Player.AttackSpeed * attackSpeed * powerupLevelRatio;
        Gamestate.Instance.Player.Energy += Gamestate.Instance.Player.MaxEnergy * energy * powerupLevelRatio;
        Gamestate.Instance.Player.MaxEnergy += Gamestate.Instance.Player.MaxEnergy * energy * powerupLevelRatio;
        Gamestate.Instance.Player.EnergyRegen += Gamestate.Instance.Player.EnergyRegen * energyRegen * powerupLevelRatio;
        Gamestate.Instance.Player.SwitchSpeed += Gamestate.Instance.Player.SwitchSpeed * switchSpeed * powerupLevelRatio * 3;
        Notify();
        AudioSource.PlayClipAtPoint(powerupSound, transform.position);
        Gamestate.Instance.Tutorial[TutorialScript.TutorialType.Powerup] = true;
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
