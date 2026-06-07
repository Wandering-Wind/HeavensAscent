using UnityEngine;

public class Intro_Dialogou : MonoBehaviour
{

    void Start()
    {
        NPC_Dialogoue.Instance.StartDialogue( new string[] {"To think i would find some lost souls here.", "Hmmm I'll only permit one of you through to Heaven.", "Good Luck!"});
        Destroy(gameObject);
    }
}

