using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    private Image image;
    private static Fade Instance;
    void Start()
    {
        if(!Instance) Instance = this;
        image = GetComponent<Image>();
    }
    public static IEnumerator DoFade(float target, float time)
    {
        float distance = target - Instance.image.color.a;
        
        while (Mathf.Abs(Instance.image.color.a - target) > 0.05f)
        {
            var color = Instance.image.color;
            color.a += distance * Time.deltaTime / time;
            Instance.image.color = color;
            yield return null;
        }
    }
}
