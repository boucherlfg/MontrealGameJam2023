using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class RespawnOnTouch : MonoBehaviour
{
    private float lastLife;
    EnemyScript me;

    public AudioClip respawnSound;
    // Start is called before the first frame update
    void Start()
    {
        me = GetComponent<EnemyScript>();
        lastLife = me.lifeCounter;
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamestate.Instance.Paused) return;
        var life = me.lifeCounter;
        if (lastLife > life)
        {
            Respawn();
        }
        lastLife = life;
    }

    void Respawn()
    {
        AudioSource.PlayClipAtPoint(respawnSound, transform.position);
        var pos = GetValidPos();
        transform.position = pos;
    }
    static Vector2 GetValidPos()
    {
        var size = Generator.size;
        var pos = Gamestate.Instance.Player.Position.Value + Random.insideUnitCircle.normalized * 20;
        while (pos.x < -size || pos.x > size || pos.y < -size || pos.y > size)
        {
            pos = Gamestate.Instance.Player.Position.Value + Random.insideUnitCircle.normalized * 20;
        }
        var hit = Physics2D.OverlapCircleAll(pos, 0.3f);
        if (hit.Length > 0) return GetValidPos();

        return pos;
    }
}
