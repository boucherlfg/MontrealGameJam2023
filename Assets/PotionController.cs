using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour
{
    public AudioClip potionSound;
    // Start is called before the first frame update
    void Start()
    {
        Inputs.Instance.Potioned += OnPotion;
    }
    void OnDestroy()
    {
        Inputs.Instance.Potioned -= OnPotion;
    }
    private void OnPotion()
    {
        if (Gamestate.Instance.Paused.Value)
        {
            return;
        }
        if (Gamestate.Instance.Player.Potions.Value <= 0) return;

        Gamestate.Instance.Player.Potions.Value--;
        Gamestate.Instance.Player.Life.Value = Mathf.Clamp(Gamestate.Instance.Player.Life.Value + Gamestate.Instance.Player.MaxLife.Value * 0.25f, 0, Gamestate.Instance.Player.MaxLife.Value);
        AudioSource.PlayClipAtPoint(potionSound, transform.position);
    }
}
