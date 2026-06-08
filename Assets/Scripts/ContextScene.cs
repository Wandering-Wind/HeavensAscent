using TMPro;
using UnityEngine;
using System.Collections;

public class ContextScene : MonoBehaviour
{
    public GameObject introPanel;
    public GameObject orbPanel;

    public CanvasGroup introCanvas;
    public CanvasGroup orbCanvas;

    public Typewriter introText;
    public Typewriter orbText;

    void Start()
    {
       
        introPanel.SetActive(true);
        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro()
    {
        yield return FadeIn(introCanvas, 1f);

        introText.StartTyping(
            "Well, it turns out being righteous and pure of heart doesn't guarantee you into heaven... \r\nFIGHTING FOR IT DOES"
        );
    }

    IEnumerator FadeIn(CanvasGroup panel, float duration)
    {
        float time = 0;
        panel.alpha = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            panel.alpha = Mathf.Lerp(0, 1, time / duration);
            yield return null;
        }

        panel.alpha = 1;
    }
}

/*
 * 
 * *Canvas
   Panel_Intro
       Text_Intro
   Panel_LightOrb
       LightOrbSprite
       Text_LightOrb
   ContinueButton


initial state
Panel_Intro        active
Panel_LightOrb     inactive
ContinueButton     inactive
 */