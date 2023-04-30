using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
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

    private bool isSprinting = false;
    private bool isExhausted = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused)
        {
            body.velocity = Vector2.zero;
            return;
        }
        Sprint();
        Move();
        Shoot();
        Potion();

        UpdateCamera();
        if (Gamestate.Instance.Player.Life <= 0)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameOverScene);
        }
    }
    private void UpdateCamera() 
    {
        var camera = Camera.main;
        var camPos = camera.transform.position;
        camPos = Vector3.Lerp(camPos, transform.position, cameraSpeed);
        camPos.z = -10;
        camera.transform.position = camPos;
    }
    private void Sprint()
    {
        isSprinting = !isExhausted && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
        if (isSprinting)
        {
            if (Gamestate.Instance.Player.Energy <= 0)
            {
                Gamestate.Instance.Player.Energy = 0;
                isSprinting = false;
                isExhausted = true;
                return;
            }
            Gamestate.Instance.Player.Energy -= Time.deltaTime;
        }
        else
        {
            Gamestate.Instance.Player.Energy += Gamestate.Instance.Player.EnergyRegen * Time.deltaTime;
            if (Gamestate.Instance.Player.Energy >= Gamestate.Instance.Player.MaxEnergy)
            {
                isExhausted = false;
                Gamestate.Instance.Player.Energy = Gamestate.Instance.Player.MaxEnergy;
            }
        }
    }
    private void Move()
    {
        var speed = !isSprinting ? Gamestate.Instance.Player.MoveSpeed : Gamestate.Instance.Player.MoveSpeed * 2;
        var x = Input.GetAxisRaw("Horizontal");
        var y = Input.GetAxisRaw("Vertical");

        body.velocity = new Vector2(x, y).normalized * speed;
        Gamestate.Instance.Position = transform.position;
    }
    private void Shoot()
    {
        UpdateAttackSlider();
        if (Input.GetMouseButton(0) && Gamestate.Instance.AttackCooldown <= 0)
        {
            Gamestate.Instance.AttackCooldown = Gamestate.Instance.Player.AttackSpeed;
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var vector = ((Vector2)(mousePos - transform.position)).normalized;
            var attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
            attack.GetComponent<AttackScript>().velocity = vector;
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
        Gamestate.Instance.AttackCooldown -= Time.deltaTime;

        void UpdateAttackSlider()
        {
            var slider = GetComponentInChildren<WorldSlider>(true);
            slider.gameObject.SetActive(Gamestate.Instance.AttackCooldown > 0);
            slider.value = 1 - (Gamestate.Instance.AttackCooldown / Gamestate.Instance.Player.AttackSpeed);
        }
    }
    private void Potion()
    {
        if (!Input.GetKeyUp(KeyCode.F)) return;

        if (Gamestate.Instance.Potions <= 0) return;

        Gamestate.Instance.Potions--;
        Gamestate.Instance.Player.Life = Mathf.Clamp(Gamestate.Instance.Player.Life + Gamestate.Instance.Player.MaxLife * 0.25f, 0, Gamestate.Instance.Player.MaxLife);
        AudioSource.PlayClipAtPoint(potionSound, transform.position);
    }
}
