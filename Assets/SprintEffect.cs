using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintEffect : MonoBehaviour
{
    public AudioClip startSound, endSound;
    // Start is called before the first frame update
    void Start()
    {
        Gamestate.Instance.Player.IsSprinting.Changed += IsSprinting_Changed;
    }
    void OnDestroy()
    {
        Gamestate.Instance.Player.IsSprinting.Changed -= IsSprinting_Changed;
    }

    private void IsSprinting_Changed()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            return;
        }
        bool value = Gamestate.Instance.Player.IsSprinting;

        if (value)
        {
            StopAllCoroutines();
            AudioSource.PlayClipAtPoint(startSound, Gamestate.Instance.Player.Position.Value);
            StartCoroutine(Fade.Instance.FadeAsync(0.2f, 0.5f));
        }
        else
        {
            StopAllCoroutines();
            AudioSource.PlayClipAtPoint(endSound, Gamestate.Instance.Player.Position.Value);
            StartCoroutine(Fade.Instance.FadeAsync(0f, 0.5f));
        }
    }
}
