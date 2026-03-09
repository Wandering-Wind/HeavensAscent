using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void HomeScene()
    {
        //SceneManager.LoadScene(0); Why doesn't it allow this thoughh?
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ContextScene()
    {
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
