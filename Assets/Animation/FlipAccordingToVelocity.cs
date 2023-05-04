using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAccordingToVelocity : MonoBehaviour
{
    public SpriteRenderer rend;
    public Rigidbody2D rbody;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var velo = rbody.velocity;
        if (velo.x > 0.1) rend.flipX = true;
        else if (velo.x < -0.1) rend.flipX = false;
    }
}
