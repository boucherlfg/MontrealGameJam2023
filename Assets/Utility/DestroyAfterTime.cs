using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float time;
    public void Update()
    {
        if (time >= 0) return;
        Destroy(gameObject);
    }
}
