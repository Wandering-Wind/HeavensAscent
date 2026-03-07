using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
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
   /* public float random_Plat_Rangex;
    public float random_Plat_Rangey;*/
    public float Spawn_Time;

    public GameObject Platform1;
    private GameObject Platform1Temp;
    public GameObject Platform2;
    private GameObject Platform2Temp;
    public GameObject Platform3;
    private GameObject Platform3Temp;
    public GameObject Platform4;
    private GameObject Platform4Temp;


    public List<Transform> platformSpawnPoints;
    public List<Transform> portalMovePoints;


    public void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player_01_Soul"))
        {
            P1_Score += 1;
            P1_Text.text = P1_Score.ToString();
            Destroy(collision.gameObject);
            P1_scoring = true;
        }

        if (collision.CompareTag("Player_02_Soul"))
        {
            P2_Score += 1;
            P2_Text.text = P2_Score.ToString();
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
        Vector2 OrbSpawnPos = new Vector2(transform.position.x + random_Orbx, transform.position.y + random_Orby);
        currLightOrb = Instantiate(LightOrb, OrbSpawnPos, Quaternion.identity);
    }
    public void Spawn_Platfroms()
    {
        if (Platform1Temp != null) Destroy(Platform1Temp);
        if (Platform2Temp != null) Destroy(Platform2Temp);
        if (Platform3Temp != null) Destroy(Platform3Temp);
        if (Platform4Temp != null) Destroy(Platform4Temp);

        List<Transform> tempSpawnPoints = new List<Transform>(platformSpawnPoints);

        Transform spawn1 = tempSpawnPoints[Random.Range(0, tempSpawnPoints.Count)];
        tempSpawnPoints.Remove(spawn1);

        Transform spawn2 = tempSpawnPoints[Random.Range(0, tempSpawnPoints.Count)];
        tempSpawnPoints.Remove(spawn2);

        Transform spawn3 = tempSpawnPoints[Random.Range(0, tempSpawnPoints.Count)];
        tempSpawnPoints.Remove(spawn3);

        Transform spawn4 = tempSpawnPoints[Random.Range(0, tempSpawnPoints.Count)];

        Platform1Temp = Instantiate(Platform1, spawn1.position, Quaternion.identity);
        Platform2Temp = Instantiate(Platform2, spawn2.position, Quaternion.identity);
        Platform3Temp = Instantiate(Platform3, spawn3.position, Quaternion.identity);
        Platform4Temp = Instantiate(Platform4, spawn4.position, Quaternion.identity);

    }
    public void ScoreReset()
    {
        if (P1_scoring)
        {
            P1.transform.position = P1_Start_Pos.transform.position;
            P1_scoring = false;
        }

        if (P2_scoring)
        {
            P2.transform.position = P2_Start_Pos.transform.position;
            P2_scoring = false;
        }
        Spawn_Platfroms();
        StartCoroutine(MovePortal());
    }
    // Time.timeScale = 0;
    // StartCoroutine(StartAgian(3));
    /*IEnumerator StartAgian(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1;
        Spawn_Platfroms();
    }*/

    IEnumerator MovePortal()
    {
        while (true)
        {
            if (portalMovePoints.Count > 0 && gameObject != null)
            {
                Transform target = portalMovePoints[Random.Range(0, portalMovePoints.Count)];
                gameObject.transform.position = target.position;
            }

            yield return new WaitForSeconds(10f); 
        }
    }
}
