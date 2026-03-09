using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource_Score;
    public AudioSource audioSource_Winner;
    public AudioClip Score_Sound;
    public AudioClip WinnerAAAH;


    public void PlayScore()
    {
        audioSource_Score.PlayOneShot(Score_Sound);
    }
    public void PlayWinnerAAAH()
    {
        audioSource_Winner.PlayOneShot(WinnerAAAH);
    }
}
