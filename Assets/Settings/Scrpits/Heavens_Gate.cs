using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Heavens_Gate : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;

    public GameObject P1_Start_Pos;
    public GameObject P2_Start_Pos;

    private bool P1_scoring = false;
    private bool P2_scoring = false;

    public float P1_Score;
    public float P2_Score;

    public TextMeshProUGUI P1_Text;
    public TextMeshProUGUI P2_Text;

    public GameObject LightOrb;
    private GameObject currLightOrb;
    public float random_Ord_Rangex;
    public float random_Ord_Rangey;
    public float Spawn_Time;

    public List<GameObject> Platforms;
    

    public void Start()
    {
        StartCoroutine(SpawnRoutine());
        Spawn_Platfroms();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player_01_Soul") )
        {
            P1_Score += 1;
            P1_Text.text = P1_Score.ToString();
            Destroy(collision.gameObject);
            P1_scoring = true;
        }
        if (collision.CompareTag("Player_02_Soul"))
        {
            P2_Score += 1;
            P2_Text.text = P1_Score.ToString();
            Destroy(collision.gameObject);
            P2_scoring = true;
        }
        ScoreReset();
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
    public void Spawn_Platfroms()
    {

        foreach(GameObject Plat in Platforms)
        {
            float Plat_Orbx = Random.Range(-random_Ord_Rangex, random_Ord_Rangex);
            float Plat_Orby = Random.Range(-random_Ord_Rangey, random_Ord_Rangey);
            Vector2 PlatSpawnPos = new Vector2(Plat_Orbx, Plat_Orby);
            Instantiate(Plat, PlatSpawnPos, Quaternion.identity);
        }
    }
    public void ScoreReset()
    {
        if (P1_scoring)
        {
            P1.transform.position = P1_Start_Pos.transform.position;
            P1_scoring = false;
        }
        else if (P2_scoring)
        {
            P2.transform.position = P2_Start_Pos.transform.position;
            P2_scoring = false;
        }
        Time.timeScale = 0;
        StartCoroutine(StartAgian(3));
    }
    IEnumerator StartAgian(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;
    }
}
