using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Heaven_Gate_Stage_02 : MonoBehaviour
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
    private GameObject currLightOrb2;
    private GameObject currLightOrb3;
    public float random_Ord_Rangex;
    public float random_Ord_Rangey;
    /* public float random_Plat_Rangex;
     public float random_Plat_Rangey;*/
    public float Spawn_Time;


    public int scoreToWin = 5;
    public GameObject winPanel_P1;
    public GameObject winPanel_P2;
    public GameObject Restart;
    public GameObject replay;

    public Animator Anim_P1;
    public Animator Anim_P2;

    public bool gameEnded;

    public AudioManager AM;

    public void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_01_Controls p1 = P1.GetComponent<Player_01_Controls>();
        Player_02_Controls p2 = P2.GetComponent<Player_02_Controls>();
        if (gameEnded) return;

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

        // Only allow hits if object is moving downward
        if (rb != null && rb.linearVelocity.y >= 0)
            return;

        if (collision.CompareTag("Player_01_Soul"))
        {
            AM.PlayScore();
            P1_Score++;
            P1_Text.text = P1_Score.ToString();
            UpdateLoseBuffs();
            Destroy(collision.gameObject);

            if (P1_Score >= scoreToWin)
            {
                if (p1.selectedClass == PlayerClassEnum.Angel)
                {
                    NPC_Dialogoue.Instance.StartDialogue(new Dialogou_Line[]
                    {
                        new Dialogou_Line { speakerID = "Angel", text = "Well Done You Are Truly Blessed By The Angels." }
                    });
                }
                if (p1.selectedClass == PlayerClassEnum.Devil)
                {
                    NPC_Dialogoue.Instance.StartDialogue(new Dialogou_Line[]
                    {
                        new Dialogou_Line { speakerID = "Devil", text = "You'd even sell a part of you to get here..." },
                        new Dialogou_Line { speakerID = "Devil", text = "Oh well... Congratulations!" }
                    });
                }
                AM.PlayWinnerAAAH();
                gameEnded = true;
                winPanel_P1.SetActive(true);
                Restart.SetActive(true);
                replay.SetActive(true);
                Anim_P1.SetTrigger("Win");
                Anim_P2.SetTrigger("Lose");
                DisablePlayers();
            }

            P1_scoring = true;
            ScoreReset();
        }
        else if (collision.CompareTag("Player_02_Soul"))
        {
            AM.PlayScore();
            P2_Score++;
            P2_Text.text = P2_Score.ToString();
            UpdateLoseBuffs();
            Destroy(collision.gameObject);

            if (P2_Score >= scoreToWin)
            {
                if (p1.selectedClass == PlayerClassEnum.Angel)
                {
                    NPC_Dialogoue.Instance.StartDialogue(new Dialogou_Line[]
                    {
        new Dialogou_Line { speakerID = "Angel", text = "Well Done You Are Truly Blessed By The Angels." }
                    });
                }
                if (p2.selectedClass == PlayerClassEnum.Devil)
                {
                    NPC_Dialogoue.Instance.StartDialogue(new Dialogou_Line[]
                    {
        new Dialogou_Line {speakerID = "Devil", text = "You'd even sell a part of you to get here..." },
        new Dialogou_Line { speakerID = "Devil", text = "Oh well... Congratulations!" }
                    });
                }
                AM.PlayWinnerAAAH();
                gameEnded = true;
                winPanel_P2.SetActive(true);
                Restart.SetActive(true);
                replay.SetActive(true);
                Anim_P2.SetTrigger("Win");
                Anim_P1.SetTrigger("Lose");
                DisablePlayers();
            }

            P2_scoring = true;
            ScoreReset();
        }
    }
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            Spawn_Light_orbs1();
            Spawn_Light_orbs2();
            Spawn_Light_orbs3();
            yield return new WaitForSeconds(Spawn_Time);
        }
    }

    public void Spawn_Light_orbs1()
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
    public void Spawn_Light_orbs2()
    {
        if (currLightOrb2 != null)
        {
            Destroy(currLightOrb2);
        }
        float random_Orbx = Random.Range(-random_Ord_Rangex, random_Ord_Rangex);
        float random_Orby = Random.Range(-random_Ord_Rangey, random_Ord_Rangey);
        Vector2 OrbSpawnPos = new Vector2(transform.position.x + random_Orbx, transform.position.y + random_Orby);
        currLightOrb2 = Instantiate(LightOrb, OrbSpawnPos, Quaternion.identity);
    }
    public void Spawn_Light_orbs3()
    {
        if (currLightOrb3 != null)
        {
            Destroy(currLightOrb3);
        }
        float random_Orbx = Random.Range(-random_Ord_Rangex, random_Ord_Rangex);
        float random_Orby = Random.Range(-random_Ord_Rangey, random_Ord_Rangey);
        Vector2 OrbSpawnPos = new Vector2(transform.position.x + random_Orbx, transform.position.y + random_Orby);
        currLightOrb3 = Instantiate(LightOrb, OrbSpawnPos, Quaternion.identity);
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
    }


    void DisablePlayers()
    {
        P1.GetComponent<Player_01_Controls>().enabled = false;
        P2.GetComponent<Player_02_Controls>().enabled = false;

        P1.GetComponent<PlayerInput>().actions.Disable();
        P2.GetComponent<PlayerInput>().actions.Disable();
    }

    public void UpdateLoseBuffs()
    {
        Player_01_Controls p1 = P1.GetComponent<Player_01_Controls>();
        Player_02_Controls p2 = P2.GetComponent<Player_02_Controls>();

        float diff = Mathf.Abs(P1_Score - P2_Score);

        float baseBuff = 1f + (diff * 0.15f);

        baseBuff = Mathf.Clamp(baseBuff, 1f, 2.5f);

        p1.LoseMulti = 1f;
        p2.LoseMulti = 1f;

        p1.SoulSizeMultiplier = 1f;
        p2.SoulSizeMultiplier = 1f;

        if (P1_Score < P2_Score)
        {
            p1.LoseMulti = baseBuff;
            p2.LoseMulti = 1f;

            if (p1.selectedClass == PlayerClassEnum.Angel)
            {
                p1.SoulSizeMultiplier = Mathf.Clamp(1f + diff * 0.1f, 1f, 1.5f);
            }
        }
        else if (P2_Score < P1_Score)
        {
            p2.LoseMulti = baseBuff;
            p1.LoseMulti = 1f;

            if (p2.selectedClass == PlayerClassEnum.Angel)
            {
                p2.SoulSizeMultiplier = Mathf.Clamp(1f + diff * 0.1f, 1f, 1.5f);
            }
        }
        else
        {
            p1.LoseMulti = 1f;
            p2.LoseMulti = 1f;
        }
        Debug.Log($"P1 Buff: {p1.LoseMulti} | P2 Buff: {p2.LoseMulti}");
    }
}