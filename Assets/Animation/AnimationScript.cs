using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rbody;
    public SpriteRenderer rend;

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
