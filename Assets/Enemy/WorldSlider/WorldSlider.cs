using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSlider : MonoBehaviour
{
    [Range(0, 1)]
    public float value;
    public Transform background;
    public Transform fill;

    void Update()
    {
        fill.localScale = new Vector3(value, 1, 1);
    }
}
