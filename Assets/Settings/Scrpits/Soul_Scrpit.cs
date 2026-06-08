using System.Collections;
using UnityEngine;

public class Soul_Scrpit : MonoBehaviour
{

    public Player_02_Controls player2;
    public GameObject hitVFX;
    public float vfxLifetime = 1f;
    public Vector2 hitVFXOffset;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player_01_Controls p1 = collision.gameObject.GetComponent<Player_01_Controls>();

        if (p1 == null) return;
        if (hitVFX != null)
        {
            Vector3 hitPos = collision.transform.position + (Vector3)hitVFXOffset;

            GameObject vfx = Instantiate(hitVFX, hitPos, Quaternion.identity);
            Destroy(vfx, vfxLifetime);
        }

        if (player2.selectedClass == PlayerClassEnum.Devil)
        {
            if (p1.currChargep1 > 0)
            {
                p1.currChargep1--;
                player2.currChargep2++;
            }

            player2.StartCoroutine(StunPlayer1(p1, 2f));
        }
    }

    IEnumerator StunPlayer1(Player_01_Controls target, float duration)
    {
        target.isStunned = true;


        yield return new WaitForSeconds(duration);

        target.isStunned = false;
    }
}