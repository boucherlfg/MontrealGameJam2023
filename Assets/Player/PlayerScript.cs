using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    bool shooting;
    public float life = 5;
    public float moveSpeed = 4;
    public float damage = 1;
    public float attackSpeed = 1;
    public float energy = 3;
    public float energyRegen = 1;
    public float switchSpeed = 5;

    public GameObject attackPrefab;
    public float cameraSpeed = 0.9f;

    private Rigidbody2D body;

    private Vector2 direction;
    [Header("sounds")]
    public AudioClip attackSound;
    public AudioClip deathSound;
    public AudioClip potionSound;

    [Scene]
    public string gameOverScene;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        Gamestate.Instance.Initialize(life, energy, energyRegen, switchSpeed, moveSpeed, damage, attackSpeed);
        Inputs.Instance.Clicked += OnClick;
        Inputs.Instance.Potioned += OnPotion;
        Gamestate.Instance.Player.Life.Changed += Life_Changed;
    }
    void OnDestroy()
    {
        Inputs.Instance.Clicked -= OnClick;
        Inputs.Instance.Potioned -= OnPotion;
        Gamestate.Instance.Player.Life.Changed -= Life_Changed;
    }

    private void Life_Changed()
    {
        if (Gamestate.Instance.Player.Life.Value <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameOverScene);
        }
    }

    

    private void OnPotion()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            return;
        }
        if (Gamestate.Instance.Player.Potions.Value <= 0) return;

        Gamestate.Instance.Player.Potions.Value--;
        Gamestate.Instance.Player.Life.Value = Mathf.Clamp(Gamestate.Instance.Player.Life.Value + Gamestate.Instance.Player.MaxLife.Value * 0.25f, 0, Gamestate.Instance.Player.MaxLife.Value);
        AudioSource.PlayClipAtPoint(potionSound, transform.position);
    }

    private void OnClick(bool value)
    {
        shooting = value;
    }


    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            body.velocity = Vector2.zero;
            return;
        }
        Move();
        Shoot();

        UpdateAttackSlider();
        UpdateCamera();
    }
    private void UpdateCamera() 
    {
        var camera = Camera.main;
        var camPos = camera.transform.position;
        var z = camPos.z;
        camPos = Vector3.Lerp(camPos, transform.position, cameraSpeed);
        camPos.z = z;
        camera.transform.position = camPos;
    }
    
    private void Move()
    {
        var speed = !Gamestate.Instance.Player.IsSprinting ? Gamestate.Instance.Player.MoveSpeed.Value : Gamestate.Instance.Player.MoveSpeed.Value * 2;
        var move = Inputs.Instance.Move;

        body.velocity = move.normalized * speed;
        Gamestate.Instance.Player.Position.Value = transform.position;
    }
    private void Shoot()
    {
        Gamestate.Instance.Player.AttackCooldown.Value -= (Gamestate.Instance.Player.IsSprinting ? Gamestate.Instance.Player.AttackSpeed.Value * 2 : Gamestate.Instance.Player.AttackSpeed.Value) * Time.deltaTime;

        UpdateAttackSlider();

        if (!shooting || Gamestate.Instance.Player.AttackCooldown.Value > 0.01f) return;

        Gamestate.Instance.Player.AttackCooldown.Value = 1;
        
        var attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        attack.GetComponent<AttackScript>().velocity = GetDirection();
        AudioSource.PlayClipAtPoint(attackSound, transform.position);
    }
    Vector2 GetDirection()
    {

        var pos = Camera.main.ScreenToWorldPoint(Inputs.Instance.MousePosition);
        direction = pos - transform.position;

        return direction.normalized;
    }
    void UpdateAttackSlider()
    {
        var slider = GetComponentInChildren<WorldSlider>(true);
        slider.gameObject.SetActive(Gamestate.Instance.Player.AttackCooldown.Value > 0);
        slider.value = 1 - Gamestate.Instance.Player.AttackCooldown.Value;
    }
}
