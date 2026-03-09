using TMPro;
using UnityEngine;
using System.Collections;
using System;

public class Typewriter : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float typingSpeed = 0.05f;

    public void StartTyping(string message, Action onComplete = null)
    {
        StopAllCoroutines();
        StartCoroutine(TypeText(message, onComplete));
    }

    IEnumerator TypeText(string message, Action onComplete)
    {
        textComponent.text = "";

        foreach (char letter in message)
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        onComplete?.Invoke();//this is supposed to run when the typing finishes. fingers crossed
    }
}
