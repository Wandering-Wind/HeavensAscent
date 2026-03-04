using UnityEngine;

public class Light_Orbs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_01_Soul"))
        {
            Player_01_Controls player = collision.GetComponentInParent<Player_01_Controls>();
            if (player != null)
            {
                player.Orb_Absorb();
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player_02_Soul"))
        {
            Player_02_Controls player = collision.GetComponentInParent<Player_02_Controls>();
            if (player != null)
            {
                player.Orb_Absorb();
            }
            Destroy(gameObject);
        }
    }
}
