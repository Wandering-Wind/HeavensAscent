using UnityEngine;
using System.Collections;

public class FadeIn : MonoBehaviour
{
    IEnumerator FadePanel(CanvasGroup panel, float duration)
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
    ///Fahhhhhh, this works better in ContextScene script. I dunno what I was thinking
    ///-Amina
}
