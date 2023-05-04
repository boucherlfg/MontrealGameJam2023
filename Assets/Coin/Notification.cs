using UnityEngine;

public class Notification : SingletonBehaviour<Notification>
{
    public TMPro.TMP_Text[] lines;
    public void Create(string message)
    {
        for (int i = 1; i < lines.Length; i++)
        {
            lines[i - 1].text = lines[i].text;
        }
        lines[lines.Length - 1].text = message;
    }
}