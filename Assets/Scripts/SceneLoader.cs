using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public float dialogueDuration = 4f;
    public void HomeScene()
    {
        //SceneManager.LoadScene(0); Why doesn't it allow this thoughh?
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ContextScene()
    {
        StartCoroutine(ContextSceneRoutine());
    }

    IEnumerator ContextSceneRoutine()
    {
        dialoguePanel.SetActive(true);

        dialogueText.text = "Go fetch the next ones in line, peasant, I'm getting bored!";

        yield return new WaitForSeconds(dialogueDuration);

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void ControlsScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void GameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
