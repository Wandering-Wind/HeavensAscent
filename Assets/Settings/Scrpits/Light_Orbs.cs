using UnityEngine;

public class Light_Orbs : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if it directly hit a player
        Player_01_Controls p1 = collision.GetComponentInParent<Player_01_Controls>();
        if (p1 != null)
        {
            p1.Orb_Absorb();
            Destroy(gameObject);
            return;
        }

        Player_02_Controls p2 = collision.GetComponentInParent<Player_02_Controls>();
        if (p2 != null)
        {
            p2.Orb_Absorb();
            Destroy(gameObject);
            return;
        }

        Soul_Scrpit owner2 = collision.GetComponent<Soul_Scrpit>();
        if (owner2 != null && owner2.player2 != null)
        {
            owner2.player2.Orb_Absorb();
            Destroy(gameObject);
        }
        SoulScrpit_01 owner1 = collision.GetComponent<SoulScrpit_01>();
        if (owner1 != null && owner1.player1 != null)
        {
            owner1.player1.Orb_Absorb();
            Destroy(gameObject);
        }
    }
}