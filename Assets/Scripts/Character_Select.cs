using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Character_Select : MonoBehaviour
{

    public TMP_Text playerText;
    public TMP_Text classText;

    private PlayerClassEnum[] classes =
    {
        PlayerClassEnum.Devil,
        PlayerClassEnum.Angel
    };

    private int classIndex = 0;
    private int currentPlayer = 1;

    private void Start()
    {
        UpdateUI();
    }

    public void NextClass()
    {
        classIndex++;

        if (classIndex >= classes.Length)
            classIndex = 0;

        UpdateUI();
    }

    public void PreviousClass()
    {
        classIndex--;

        if (classIndex < 0)
            classIndex = classes.Length - 1;

        UpdateUI();
    }

    public void ConfirmSelection()
    {
        if (currentPlayer == 1)
        {
            SelectManager.Instance.player1Class = classes[classIndex];

            currentPlayer = 2;
            classIndex = 0;

            UpdateUI();
        }
        else
        {
            SelectManager.Instance.player2Class = classes[classIndex];

            UnityEngine.SceneManagement.SceneManager.LoadScene("Nhlanzeko");
        }
    }

    void UpdateUI()
    {
        playerText.text = $"Player {currentPlayer} Select Class";
        classText.text = classes[classIndex].ToString();
    }
}
