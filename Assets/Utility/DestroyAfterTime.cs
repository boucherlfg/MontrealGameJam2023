using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time;
    public void Update()
    {
        time -= Time.deltaTime;
        if (time >= 0) return;
        Destroy(gameObject);
    }
}
