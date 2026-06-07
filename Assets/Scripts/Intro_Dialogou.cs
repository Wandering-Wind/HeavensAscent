using UnityEngine;

public class Intro_Dialogou : MonoBehaviour
{
    void Start()
    {
        NPC_Dialogoue.Instance.StartDialogue(new Dialogou_Line[]
        {
            new Dialogou_Line { speakerID = "Angel", text = "You should not be here..." },
            new Dialogou_Line { speakerID = "Angel", text = "Only one soul may pass." },
            new Dialogou_Line { speakerID = "Devil", text = "HaHaHa Let's see which soul is mightier" },
            new Dialogou_Line { speakerID = "Devil", text = "Let’s see who is worthy." }
        });
    }
}

