using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public TMPro.TMP_Text label;
    public Image image;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitUntil(() => Gamestate.Instance.Environments.Count > 0);
        Gamestate.Instance.Environment.Score.Changed += UpdateDisplay;
        Gamestate.Instance.Index.Changed += Index_Changed;
        UpdateDisplay();
    }

    private void Index_Changed()
    {
        Gamestate.Instance.Environments.ForEach(env => env.Score.Changed -= UpdateDisplay);
        Gamestate.Instance.Environment.Score.Changed += UpdateDisplay;
        UpdateDisplay();
    }

    void OnDestroy()
    {
        Gamestate.Instance.Environments.ForEach(env => env.Score.Changed -= UpdateDisplay);
        Gamestate.Instance.Index.Changed -= Index_Changed;
    }

    private void UpdateDisplay()
    {
        var env = Gamestate.Instance.Environment;
        int value = env.Score.Value;
        label.text = value + "";
        image.sprite = env.Data.scoreSprite;
    }
}
