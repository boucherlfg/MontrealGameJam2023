using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public enum TutorialType
    {
        Score = 0,
        Powerup = 1,
        Enemy = 2,
        Potion = 3
    }
    public TutorialType type;
    // Start is called before the first frame update
    void Update()
    {
        gameObject.SetActive(!Gamestate.Instance.Tutorial[type]);
    }

}
