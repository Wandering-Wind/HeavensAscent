using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

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

    private float moveCooldown = 0.25f;
    private float nextMoveTime;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        Gamepad pad = currentPlayer == 1
            ? Gamepad.all[0]
            : Gamepad.all[1];

        if (pad == null) return;

        if (Time.time > nextMoveTime)
        {
            if (pad.leftStick.x.ReadValue() > 0.5f)
            {
                NextClass();
                nextMoveTime = Time.time + moveCooldown;
            }
            else if (pad.leftStick.x.ReadValue() < -0.5f)
            {
                PreviousClass();
                nextMoveTime = Time.time + moveCooldown;
            }
        }

        if (pad.buttonSouth.wasPressedThisFrame)
        {
            ConfirmSelection();
        }
    }

    void NextClass()
    {
        classIndex++;

        if (classIndex >= classes.Length)
            classIndex = 0;

        UpdateUI();
    }

    void PreviousClass()
    {
        classIndex--;

        if (classIndex < 0)
            classIndex = classes.Length - 1;

        UpdateUI();
    }

    void ConfirmSelection()
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
        playerText.text = ("Player {currentPlayer} Select Class");
        classText.text = classes[classIndex].ToString();
    }
}
