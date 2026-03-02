using System.Collections;
using TMPro;
using UnityEngine;


public class Heavens_Gate : MonoBehaviour
{
    public float P1_Score;
    public float P2_Score;

    public TextMeshProUGUI P1_Text;
    public TextMeshProUGUI P2_Text;

    public GameObject LightOrb;
    private GameObject currLightOrb;
    public float random_Ord_Rangex;
    public float random_Ord_Rangey;
    public float Spawn_Time;
    public int OrbCount = 5;

    public void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

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
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn_Light_orbs();
            yield return new WaitForSeconds(Spawn_Time);
        }
    }

    public void Spawn_Light_orbs()
    {
            if (currLightOrb != null)
            {
                Destroy(currLightOrb);
            }
            float random_Orbx = Random.Range(-random_Ord_Rangex, random_Ord_Rangex);
            float random_Orby = Random.Range(-random_Ord_Rangey, random_Ord_Rangey);
            Vector2 OrbSpawnPos = new Vector2(random_Orbx, random_Orby);
            currLightOrb = Instantiate(LightOrb, OrbSpawnPos, Quaternion.identity);
    }
}
