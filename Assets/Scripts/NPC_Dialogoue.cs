using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPC_Dialogoue : MonoBehaviour
{
    public static NPC_Dialogoue Instance;

    [Header("Players")]
    public GameObject player1;
    public GameObject player2;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Dialogue Settings")]
    public float lineDuration = 3f;

    private string[] dialogueLines;
    private int currentLine;
    private bool dialogueActive;

    public bool IsDialogueActive => dialogueActive;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void StartDialogue(string[] lines)
    {
        if (lines == null || lines.Length == 0)
            return;

        dialogueLines = lines;
        currentLine = 0;
        dialogueActive = true;

        dialoguePanel.SetActive(true);
        dialogueText.text = dialogueLines[currentLine];

        if (player1 != null)
            player1.GetComponent<PlayerInput>().enabled = false;

        if (player2 != null)
            player2.GetComponent<PlayerInput>().enabled = false;

        StartCoroutine(AutoDialogue());
    }

    private IEnumerator AutoDialogue()
    {
        while (dialogueActive)
        {
            yield return new WaitForSecondsRealtime(lineDuration);

            if (dialogueActive)
                NextLine();
        }
    }

    public void NextLine()
    {
        currentLine++;

        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = dialogueLines[currentLine];
    }

    public void EndDialogue()
    {
        dialogueActive = false;

        dialoguePanel.SetActive(false);

        if (player1 != null)
            player1.GetComponent<PlayerInput>().enabled = true;

        if (player2 != null)
            player2.GetComponent<PlayerInput>().enabled = true;
    }
}