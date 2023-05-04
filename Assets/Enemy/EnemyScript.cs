using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public AudioClip spawnNoise;
    public string visibleName;
    public float life = 3;
    [HideInInspector]
    public float lifeCounter = 3;

    public float speed = 3;
    public float attack = 1;
    public float attackSpeed = 1;

    private Rigidbody2D body;
    private float attackCounter = 0;

    public WorldSlider lifeSlider;

    public AudioClip attackSound;
    public AudioClip deathSound;
    public GameObject playerBlood;

    // Start is called before the first frame update
    void Start()
    {
        if (spawnNoise) AudioSource.PlayClipAtPoint(spawnNoise, transform.position);
        body = GetComponent<Rigidbody2D>();
        lifeSlider.gameObject.SetActive(false);
        lifeCounter = life;
        Gamestate.Instance.Props[name]++;
    }
    void OnDestroy()
    {
        Gamestate.Instance.Props[name]--;
    }

    // Update is called once per frame
    void Update()
    {
        body.velocity = Vector2.zero;
        if (Gamestate.Instance.Paused.Value) return;


        var direction = GetAvoidVector() + GetChaseVector();
        var speed = Gamestate.Instance.Player.IsSprinting ? this.speed * 0.5f : this.speed;
        body.velocity = direction.normalized * speed;

        TryAttackPlayer();

        TryDie();

        UpdateSlider();

    }
    void TryDie()
    {
        if (lifeCounter <= 0)
        {
            Notification.Instance.Create("you just killed a " + visibleName + "!");
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            Gamestate.Instance.Kills[name]++;
            Gamestate.Instance.Environment.TotalKill.Value++;
            Gamestate.Instance.Environment.Tutorial[TutorialType.Enemy] = true;
            Destroy(gameObject);
        }
    }
    void TryAttackPlayer()
    {
        var distance = Vector2.Distance(Gamestate.Instance.Player.Position, transform.position);
        if (distance < 0.5f && attackCounter < 0)
        {
            attackCounter = 1;
            Gamestate.Instance.Player.Life.Value -= attack;
            Instantiate(playerBlood, Gamestate.Instance.Player.Position.Value, Quaternion.identity);
            AudioSource.PlayClipAtPoint(attackSound, transform.position);
        }
        attackCounter -= attackSpeed * Time.deltaTime;
    }
    Vector2 GetAvoidVector()
    {
        Vector2 direction = Vector2.zero;
        foreach (Transform t in transform.parent)
        {
            if (t == transform) continue;
            var comp = t.GetComponent<EnemyScript>();
            if (!comp) continue;

            Vector2 tempDir = comp.transform.position - transform.position;
            var avoidanceFactor = transform.localScale.x;
            var diffDist = avoidanceFactor - tempDir.magnitude;
            if (diffDist > 0)
            {
                direction -= tempDir.normalized * (diffDist / avoidanceFactor) * avoidanceFactor;
            }
        }
        return direction;
    }
    Vector2 GetChaseVector()
    {
        var distVect = (Gamestate.Instance.Player.Position.Value - (Vector2)transform.position);
        return distVect.normalized;
    }
    void UpdateSlider()
    {
        lifeSlider.value = ((float)lifeCounter / (float)life);
        if (lifeCounter < life)
        {
            lifeSlider.gameObject.SetActive(true);
        }
    }
}
