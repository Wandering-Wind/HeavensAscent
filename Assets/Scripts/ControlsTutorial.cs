using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsTutorial : MonoBehaviour
{
    [Header("Button Sprites")]
    [SerializeField] private GameObject south_Button;
    [SerializeField] private GameObject east_Button;
    [SerializeField] private GameObject left_Joystick;

    [Header("UI")]
    [SerializeField] private GameObject textPanel;
    [SerializeField] private TextMeshProUGUI actionText;

    [Header("Effect")]
    [SerializeField] private GameObject sparkAnim;

    public void OnShoot(InputAction.CallbackContext context)
    {
        
        if (context.started)
        {
            ShowText(east_Button, "Hold O to charge and shoot your soul to where you're aiming");
        }
        else
        {
            HideSprites(east_Button);
        }
    }

    public void OnTeleport(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShowText(south_Button, "Hold X/South Button to teleport to your soul or into your enemy");
        }
        else
        {
            HideSprites(south_Button);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        if(input.magnitude> 0.3f)
        {
            ShowText(left_Joystick, "Move the left stick to aim!");
        }
        else
        {
            HideSprites(left_Joystick);
        }
    }

    public void ShowText(GameObject activeButtonSprite, string text)
    {
        activeButtonSprite.SetActive(true);
        textPanel.SetActive(true);
        actionText.text = text;

        sparkAnim.SetActive(true);
        sparkAnim.GetComponent<Animator>().Play("Spark");

    }

    public void HideSprites(GameObject activeButtonSprite)
    {
        activeButtonSprite.SetActive(false);
        textPanel.SetActive(false);
        sparkAnim.SetActive(false); //just to be sure
    }
}

/*
 * Left control stick: aiming shows you where your stick is pointing in the scene
East button: shoots the persons soul, shoots the soul where the player is aiming
South button: teleport the player to the soul
*/