using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class AnimationScript : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rbody;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        if (!rend) rend = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rbody.velocity.magnitude > 0.1f)
        {
            anim.Play("Walking");
        }
        else
        {
            anim.Play("Idle");
        }

        if (Mathf.Abs(rbody.velocity.x) > 0.1)
        {
            rend.flipX = rbody.velocity.x < -0.1;
        }
    }
}
