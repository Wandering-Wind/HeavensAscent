using UnityEngine;

public class Intro_Dialogou : MonoBehaviour
{
    void Start()
    {
        if (NPC_Dialogoue.Instance == null)
        {
            Debug.LogError("NPC_Dialogoue Instance is NULL");
            return;
        }

        NPC_Dialogoue.Instance.StartDialogue(new string[]
        {
        "To think I would find some lost souls here.",
        "I'll only permit one of you through to Heaven.",
        "Good luck!"
        });
    }
}

