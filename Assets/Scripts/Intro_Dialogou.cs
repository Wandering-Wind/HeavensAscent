using UnityEngine;

public class Intro_Dialogou : MonoBehaviour
{
    void Start()
    {
        NPC_Dialogoue.Instance.StartDialogue(new Dialogou_Line[]
        {
            new Dialogou_Line { speakerID = "Angel", text = "Receive a fraction of my divine spark, tiny creation. Do try not to splatter it all over the masonry." },
            new Dialogou_Line { speakerID = "Angel", text = "Embody my purity, mortal. Or fall, and let your soul feed the soil. It makes no difference to me." },
            new Dialogou_Line { speakerID = "Angel", text = "A drop of my grace should suffice for creatures of your... limited stature. Do not waste it." },
            new Dialogou_Line { speakerID = "Angel", text = "Look at how they tremble. Fight well, little things, the promised land awaits... or whatever it is we told you." },
            new Dialogou_Line { speakerID = "Angel", text = "Try to keep the blood off your robes. It ruins the aesthetic of the ascension." },
            new Dialogou_Line { speakerID = "Devil", text = "Here's a taste of real power. Try not to choke on it, meat sack." },
            new Dialogou_Line { speakerID = "Devil", text = "Make it bloody, make it fast, and don't make me look bad. I have a lot riding on this."},
            new Dialogou_Line { speakerID = "Devil", text = "I'm giving you a shot at paradise, mortal. Bore me, and I'll drag you down to the pits myself just for a laugh."},
            new Dialogou_Line { speakerID = "Devil", text = "Go on then, start hitting each other. I didn’t descend all the way down here to watch you stand around."},
            new Dialogou_Line { speakerID = "Devil", text = "Let's see if that pathetic spark of yours can actually catch fire today."},
        });
    }
}

