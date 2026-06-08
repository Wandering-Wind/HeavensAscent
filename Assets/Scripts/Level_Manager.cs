using UnityEngine;
using UnityEngine.SceneManagement;
public class Level_Manager : MonoBehaviour
{

    public void LoadLevel(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}

