using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notification : SingletonBehaviour<Notification>
{
    private Queue<string> messages = new Queue<string>();
    public GameObject notificationPrefab;

    IEnumerator Start()
    {
        float counter = 0;
        while (true)
        {
            counter += Time.deltaTime;
            if (messages.Count <= 0)
            {
                yield return null;
                continue;
            }
            if (counter < 1)
            {
                yield return null;
                continue;
            }
            counter = 0;
            InstantiateNotification();

            yield return null;
        }
    }

    private void InstantiateNotification()
    {
        var instance = Instantiate(notificationPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        var comp = instance.GetComponent<TMPro.TMP_Text>();
        comp.text = messages.Dequeue();
    }

    public void Create(string message)
    {
        messages.Enqueue(message);
    }

}