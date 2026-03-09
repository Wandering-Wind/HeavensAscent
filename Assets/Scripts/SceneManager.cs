using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    /*public void HomeScene()
    {
        SceneManager.LoadScene(0);
    }

    public void ContextScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ControlsScene()
    {
        SceneManager.LoadScene(2);
    }
    
    public void GameScene()
    {
        SceneManager.LoadScene(3);
    }*/
    public void QuitGame()
    {
        Application.Quit();
    }
}
