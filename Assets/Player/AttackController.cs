using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public AudioClip attackSound;
    private Vector2 direction;
    private bool shooting;
    public GameObject attackPrefab;
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
        var pos = Camera.main.ScreenToWorldPoint(Inputs.Instance.MousePosition);
        direction = pos - transform.position;
        return direction.normalized;
    }
}
