using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationAnimation : MonoBehaviour
{
    private TMPro.TMP_Text label;
    public float speed = 1f;
    public float duration = 5f;

    private float distance = 0;
    IEnumerator Start()
    {
        label = GetComponent<TMPro.TMP_Text>();
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        distance += Time.deltaTime / duration;
        transform.position = Gamestate.Instance.Player.Position + Vector2.up * 0.5f + Vector2.up * distance; 
        transform.localPosition += Vector3.up * Time.deltaTime / duration;
        var color = label.color;
        color.a -= Time.deltaTime / duration;
        label.color = color;
    }
}
