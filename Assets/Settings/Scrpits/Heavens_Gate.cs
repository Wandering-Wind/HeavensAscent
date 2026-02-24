using UnityEngine;

public class Heavens_Gate : MonoBehaviour
{
    public float P1_Score;
    public float P2_Score;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player_01_Soul") )
        {
            P1_Score += 1;
        }
        if (collision.CompareTag("Player_02_Soul"))
        {
            P2_Score += 1;
        }
        
    }
}
