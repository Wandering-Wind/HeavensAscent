using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Character_Select : MonoBehaviour
{

    public TMP_Text playerText;
    public TMP_Text classText;
    public TMP_Text descriptionText;
    public GameObject angelPreview;
    public GameObject devilPreview;
    public GameObject lightOrbIcon;

    private PlayerClassEnum[] classes =
    {
        PlayerClassEnum.Devil,
        PlayerClassEnum.Angel
    };

    private int classIndex = 0;
    private int currentPlayer = 1;


    public string[] gameScenes ={"Nhlanzeko","Stage_02","Stage_03"};

    private string[] classDescriptions =
   {
         // Demon type shii
        "<i>\"Take what isn't yours.\"\n\n</i>" +
        "<b>Playstyle</b>\n" +
        "Fast and aggressive. Strike the Angel's soul to drain their charge and leave them stranded.\n\n" +
        "<b>Comeback Mechanic</b>\n" +
        "Collisions grow your soul's size and speed. More chaos, more power.\n\n" +
        "<b>Watch out for: </b>" +
        "Light Orbs stun you and shrink your soul.",

        // Angel
        "<i>\"Heaven favours the patient.\"\n\n</i>" +
        "<b>Playstyle</b>\n" +
        "Start with more charges for more throws and teleports. Collect Light Orbs to restore them.\n\n" +
        "<b>Comeback Mechanic</b>\n" +
        "Falling behind? Your soul grows larger, making the Heaven's Gate easier to hit.\n\n" +
        "<b>Watch out for: </b>" +
        "Demons stun you on contact. Keep moving."
    };

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
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level_Select");
        }
    }

    void UpdateUI()
    {
        playerText.text = $"Player {currentPlayer}";
        classText.text = classes[classIndex].ToString();
        descriptionText.text = classDescriptions[classIndex];

        if (angelPreview != null)
            angelPreview.SetActive(false);

        if (devilPreview != null)
            devilPreview.SetActive(false);

        switch (classes[classIndex])
        {
            case PlayerClassEnum.Angel:
                if (angelPreview != null)
                    angelPreview.SetActive(true);
                break;

            case PlayerClassEnum.Devil:
                if (devilPreview != null)
                    devilPreview.SetActive(true);
                break;
        }
    }

    /*public string[] classDescriptions ={ "Devil", "Angel" };

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

            int randomScene = Random.Range(0, gameScenes.Length);
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameScenes[randomScene]);
        }
    }

    void UpdateUI()
    {
        playerText.text = $"Player {currentPlayer} Select Class";
        classText.text = classes[classIndex].ToString();
        descriptionText.text = classDescriptions[classIndex];
    }
    */
}
