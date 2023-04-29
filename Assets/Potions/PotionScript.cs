using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Gamestate.Instance.Potions += 1;
        Destroy(gameObject);
    }
}
