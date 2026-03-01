using TMPro;
using UnityEngine;


public class Heavens_Gate : MonoBehaviour
{
    public float P1_Score;
    public float P2_Score;

    public TextMeshProUGUI P1_Text;
    public TextMeshProUGUI P2_Text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player_01_Soul") )
        {
            P1_Score += 1;
            P1_Text.text = P1_Score.ToString();
        }
        if (collision.CompareTag("Player_02_Soul"))
        {
            P2_Score += 1;
            P2_Text.text = P1_Score.ToString();
        }
        
    }
}
