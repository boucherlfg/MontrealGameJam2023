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

    public AudioClip powerupSound;

    void Start()
    {
        Notification.Instance.Create("a powerup has just appeared!");
        Gamestate.Instance.Props[name]++;
    }
    void OnDestroy()
    {
        Gamestate.Instance.Props[name]--;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerScript>();
        if (!player) return;

        Gamestate.Instance.Player.Life.Value += Gamestate.Instance.Player.MaxLife.Value * 0.1f;
        Gamestate.Instance.Player.MaxLife.Value += Gamestate.Instance.Player.MaxLife.Value * 0.1f;
        Gamestate.Instance.Player.MoveSpeed.Value += Gamestate.Instance.Player.MoveSpeed.Value * 0.03f;
        Gamestate.Instance.Player.Damage.Value += Gamestate.Instance.Player.Damage.Value * 0.05f;
        Gamestate.Instance.Player.AttackSpeed.Value += Gamestate.Instance.Player.AttackSpeed.Value * 0.03f;
        Gamestate.Instance.Player.Energy.Value += Gamestate.Instance.Player.MaxEnergy.Value * 0.1f;
        Gamestate.Instance.Player.MaxEnergy.Value += Gamestate.Instance.Player.MaxEnergy.Value * 0.1f;
        Gamestate.Instance.Player.EnergyRegen.Value += Gamestate.Instance.Player.EnergyRegen.Value * 0.15f;

        AudioSource.PlayClipAtPoint(powerupSound, transform.position);
        Gamestate.Instance.Environment.Tutorial[TutorialType.Powerup] = true;
        Destroy(gameObject);
    }
}
