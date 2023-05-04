using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourselfAsEnemy : MonoBehaviour
{
    public GameObject attackPrefab;
    private EnemyScript me;
    private Rigidbody2D rbody;

    float lastLife;

    float abilityTime = 2;
    float attackCooldown;

    public AudioClip attackSound;

    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        me = GetComponent<EnemyScript>();

        me.life = Gamestate.Instance.Player.MaxLife / 10;
        me.lifeCounter = Gamestate.Instance.Player.MaxLife / 10;
        lastLife = me.life;

        me.attack = Gamestate.Instance.Player.Damage;
        me.attackSpeed = Gamestate.Instance.Player.AttackSpeed / 2;

        me.speed = Gamestate.Instance.Player.MoveSpeed / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused)
        {
            rbody.velocity = Vector2.zero;
            return;
        }
        Move();
        Attack();

        HurtAbility();
    }

    void Attack()
    {
        attackCooldown += Time.deltaTime * me.attackSpeed;
        if (attackCooldown < 1) return;
        attackCooldown = 0;

        var playerPosition = Gamestate.Instance.Player.Position + Random.insideUnitCircle;
        var shootDirection = (playerPosition - (Vector2)transform.position).normalized;

        var attack = Instantiate(attackPrefab, transform.position, Quaternion.identity);
        var comp = attack.GetComponent<EnemyAttackScript>();
        comp.velocity = shootDirection;
        comp.damage = me.attack;
        AudioSource.PlayClipAtPoint(attackSound, transform.position);
    }

    Coroutine coroutine;
    void HurtAbility()
    {
        var life = me.lifeCounter;
        if (life < lastLife && coroutine == null)
        {
            coroutine = StartCoroutine(DoWhenHurt());
        }
        lastLife = life;
        IEnumerator DoWhenHurt()
        {
            me.speed *= 2f;
            me.attackSpeed *= 2f;
            yield return new WaitForSeconds(abilityTime);
            me.speed /= 2f;
            me.attackSpeed /= 2f;
            coroutine = null;
        }
    }

    void Move()
    {
        var move = GetChaseVector() + 0.5f*GetCenterVector() + GetDistanceVector();
        move.Normalize();
        rbody.velocity = move * me.speed;
    }


    Vector2 GetChaseVector()
    {
        var distVect = (Gamestate.Instance.Player.Position.Value - (Vector2)transform.position);
        distVect = Vector3.Cross(distVect, Vector3.forward);
        return distVect.normalized;
    }

    Vector2 GetCenterVector()
    {
        return (Vector3.zero - transform.position).normalized;
    }
    Vector2 GetDistanceVector()
    {
        const float targetDistance = 5;
        var vect = Gamestate.Instance.Player.Position - (Vector2)transform.position;
        var distance = vect.magnitude;
        if (distance > targetDistance + 3) return vect.normalized;
        else if(distance < targetDistance - 3) return -vect.normalized;
        return Vector2.zero;
    }
}
