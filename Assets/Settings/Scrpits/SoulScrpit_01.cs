using System.Collections;
using UnityEngine;

public class SoulScrpit_01 : MonoBehaviour
{
    public Player_01_Controls player1;
    public GameObject hitVFX;
    public float vfxLifetime = 1f;
    public Vector2 hitVFXOffset;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player_02_Controls p2 = collision.gameObject.GetComponent<Player_02_Controls>();

        if (p2 == null) return;

        if (hitVFX != null)
        {
            Vector3 hitPos = collision.transform.position + (Vector3)hitVFXOffset;

            GameObject vfx = Instantiate(hitVFX, hitPos, Quaternion.identity);
            Destroy(vfx, vfxLifetime);
        }

        if (player1.selectedClass == PlayerClassEnum.Devil)
        {
            if (p2.currChargep2 > 0)
            {
                p2.currChargep2--;
                player1.currChargep1++;
            }

            player1.StartCoroutine(StunPlayer2(p2, 2f));
        }
    }

    IEnumerator StunPlayer2(Player_02_Controls target, float duration)
    {
        target.isStunned = true;

        yield return new WaitForSeconds(duration);

        target.isStunned = false;
    }
}
