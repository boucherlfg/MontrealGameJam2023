using UnityEngine;

public class Notification : MonoBehaviour
{
    public TMPro.TMP_Text[] lines;
    private static Notification instance;
    void Start()
    {
        if (!instance) instance = this;
    }
    public static void Create(string message)
    {
        for (int i = 1; i < instance.lines.Length; i++)
        {
            instance.lines[i - 1].text = instance.lines[i].text;
        }
        instance.lines[instance.lines.Length - 1].text = message;
    }
}