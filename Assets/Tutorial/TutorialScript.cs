using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    
    public TutorialType type;
    // Start is called before the first frame update
    void Start()
    {
        Gamestate.Instance.Environment.Tutorial.Changed += UpdateTutorial;
        UpdateTutorial();
    }
    void OnDestroy()
    {
        Gamestate.Instance.Environment.Tutorial.Changed -= UpdateTutorial;
    }

    private void UpdateTutorial()
    {
        gameObject.SetActive(!Gamestate.Instance.Environment.Tutorial[type]);
    }
}
