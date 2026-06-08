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

    [Header("NPC Objects")]
    public GameObject Angel;
    public GameObject Devil;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI speakerText;

    [Header("Settings")]
    public float lineDuration = 3f;

    private Dialogou_Line[] dialogueLines;
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
    private void Update()
{
    if (!dialogueActive)
        return;

    if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
    {
        EndDialogue();
    }

    if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
    {
        EndDialogue();
    }
}
    public void StartDialogue(Dialogou_Line[] lines)
    {
        if (lines == null || lines.Length == 0)
            return;

        dialogueLines = lines;
        currentLine = 0;
        dialogueActive = true;

        dialoguePanel.SetActive(true);

        LockPlayers();

        ShowLine();

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
    private void ShowLine()
    {
        var line = dialogueLines[currentLine];

        dialogueText.text = line.text;
        speakerText.text = line.speakerID;

        SetActiveNPC(line.speakerID);
    }
    private void SetActiveNPC(string speakerID)
    {
        if (Angel != null) Angel.SetActive(false);
        if (Devil != null) Devil.SetActive(false);

        if (speakerID == "Angel" && Angel != null)
            Angel.SetActive(true);

        else if (speakerID == "Devil" && Devil != null)
            Devil.SetActive(true);
    }
    public void NextLine()
    {
        currentLine++;

        if (currentLine >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        ShowLine();
    }
    public void EndDialogue()
    {
        dialogueActive = false;

        dialoguePanel.SetActive(false);

        if (Angel != null) Angel.SetActive(false);
        if (Devil != null) Devil.SetActive(false);

        UnlockPlayers();
    }
    private void LockPlayers()
    {
        if (player1 != null)
            player1.GetComponent<PlayerInput>().enabled = false;

        if (player2 != null)
            player2.GetComponent<PlayerInput>().enabled = false;
    }

    private void UnlockPlayers()
    {
        if (player1 != null)
            player1.GetComponent<PlayerInput>().enabled = true;

        if (player2 != null)
            player2.GetComponent<PlayerInput>().enabled = true;
    }
}