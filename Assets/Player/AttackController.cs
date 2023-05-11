using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public float range = 10;
    public AudioClip attackSound;
    private bool shooting;
    public GameObject attackPrefab;
    private Vector2 defaultDirection = Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
        Inputs.Instance.Clicked += OnClick;
    }
    void OnDestroy()
    {
        Inputs.Instance.Clicked -= OnClick;
    }
    private void OnClick(bool value)
    {
        shooting = value;
    }
    // Update is called once per frame
    void Update()
    {
        
        Shoot();
        UpdateAttackSlider();
    }
   
    private void Shoot()
    {
        if (Gamestate.Instance.Paused) return;
        Gamestate.Instance.Player.AttackCooldown.Value -= (Gamestate.Instance.Player.IsSprinting ? Gamestate.Instance.Player.AttackSpeed.Value * 2 : Gamestate.Instance.Player.AttackSpeed.Value) * Time.deltaTime;

        if (!shooting || Gamestate.Instance.Player.AttackCooldown.Value > 0.01f) return;

        Gamestate.Instance.Player.AttackCooldown.Value = 1;

        var attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        var comp = attack.GetComponent<AttackScript>();
        comp.velocity = GetDirection();
        comp.damage = Gamestate.Instance.Player.Damage;
        AudioSource.PlayClipAtPoint(attackSound, transform.position);
    }
    void UpdateAttackSlider()
    {
        var slider = GetComponentInChildren<WorldSlider>(true);
        slider.gameObject.SetActive(Gamestate.Instance.Player.AttackCooldown.Value > 0);
        slider.value = 1 - Gamestate.Instance.Player.AttackCooldown.Value;
    }
    Vector2 GetDirection()
    {
        var move = Inputs.Instance.Move;
        if (move.magnitude >= 0.1f) defaultDirection = move;
        var enemies = EnemyScript.enemies;

        if (enemies.Count <= 0) return defaultDirection;
        EnemyScript most = null;
        float mostDist = float.MaxValue;
        foreach (var enemy in enemies)
        {
            var dist = Vector2.Distance(enemy.transform.position, transform.position);
            if (dist >= range) continue;
            if (dist >= mostDist) continue;
            most = enemy;
            mostDist = dist;
        }
        if (!most) return defaultDirection;

        defaultDirection = (most.transform.position - transform.position).normalized;
        return defaultDirection;
    }
    void OnDrawGizmos()
    {
#if UNITY_EDITOR
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, range);
#endif
    }
}
