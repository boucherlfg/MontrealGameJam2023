using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Sprite[] envScoreSprite;
    private string labelText;
    public TMPro.TMP_Text label;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        labelText = label.text;
    }

    // Update is called once per frame
    void Update()
    {
        int value = Gamestate.Instance.Player.Score;
        label.text = value + " / 10";
        image.sprite = envScoreSprite[Gamestate.Instance.EnvironmentIndex];
    }
}
