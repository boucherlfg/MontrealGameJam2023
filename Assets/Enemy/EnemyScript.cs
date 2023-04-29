using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float life = 3;
    [HideInInspector]
    public float lifeCounter = 3;

    public float speed = 3;
    public float attack = 1;
    public float attackSpeed = 1;

    private Rigidbody2D body;
    private float attackCounter = 0;

    public WorldSlider lifeSlider;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        lifeSlider.gameObject.SetActive(false);
        lifeCounter = life;
    }

    // Update is called once per frame
    void Update()
    {
        lifeSlider.value = ((float)lifeCounter / (float)life);

        var direction = Gamestate.Instance.Player.Position - (Vector2)transform.position;
        var distance = direction.magnitude;
        direction.Normalize();

        body.velocity = direction * speed;

        if (distance < 0.5f && attackCounter < 0)
        {
            attackCounter = attackSpeed;
            Gamestate.Instance.Player.Life -= attack;
        }
        attackCounter -= Time.deltaTime;

        if (lifeCounter <= 0)
        {
            Destroy(gameObject);
        }

        if (lifeCounter < life)
        {
            lifeSlider.gameObject.SetActive(true);
        }
    }
}
