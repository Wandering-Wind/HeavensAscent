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
        orbPanel.SetActive(false);

        StartCoroutine(StartIntro());
    }

    IEnumerator StartIntro()
    {
        yield return FadeIn(introCanvas, 1f);

        introText.StartTyping(
            "Ever wanted to throw your soul? Ever get sick of waiting to get to heaven? Why not see if you're daring enough to shoot your soul into the portal to get to heaven? ",
            ShowOrbPanel
        );
    }

    void ShowOrbPanel()
    {
        //introPanel.SetActive(false);
        orbPanel.SetActive(true);

        StartCoroutine(ShowOrb());
    }

    IEnumerator ShowOrb()
    {
        yield return FadeIn(orbCanvas, 1f);

        orbText.StartTyping(
            "Beware: you only have 5 teleports until you exhaust your power and have to wait to recharge\r\n. Get life orbs to get one more charge. Hint: This is a Light Orb. Shoot it to teleport across the battlefield."
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