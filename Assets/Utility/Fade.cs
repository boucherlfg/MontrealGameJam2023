using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : SingletonBehaviour<Fade>
{
    private Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }
    public IEnumerator FadeAsync(float target, float time)
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
