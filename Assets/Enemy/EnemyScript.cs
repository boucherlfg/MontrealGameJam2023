using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public static List<EnemyScript> enemies = new List<EnemyScript>();
    public string visibleName;
    public float life = 3;
    [HideInInspector]
    public float lifeCounter = 3;

    public float speed = 3;
    public float attack = 1;
    public float attackSpeed = 1;

    private float attackCounter = 0;

    public WorldSlider lifeSlider;

    public AudioClip attackSound;
    public AudioClip deathSound;
    public GameObject playerBlood;

    // Start is called before the first frame update
    void Start()
    {
        lifeSlider.gameObject.SetActive(false);
        lifeCounter = life;
        
        Gamestate.Instance.Props[name]++;
    }
    void OnEnable()
    {
        enemies.Add(this);
    }
    void OnDisable()
    {
        enemies.Remove(this);
    }
    void OnDestroy()
    {
        
        Gamestate.Instance.Props[name]--;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused.Value) return;

        TryAttackPlayer();

        TryDie();

        UpdateSlider();

    }
    void TryDie()
    {
        if (lifeCounter <= 0)
        {
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
  
    void UpdateSlider()
    {
        lifeSlider.value = ((float)lifeCounter / (float)life);
        if (lifeCounter < life)
        {
            lifeSlider.gameObject.SetActive(true);
        }
    }
}
