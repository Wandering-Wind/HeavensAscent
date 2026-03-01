using UnityEngine;

public class Light_Orbs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_01_Soul"))
        {

        }
    }
}
