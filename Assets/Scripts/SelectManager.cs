using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance;

    public PlayerClassEnum player1Class;
    public PlayerClassEnum player2Class;

    private void Awake()
    {
        {
            if(Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
