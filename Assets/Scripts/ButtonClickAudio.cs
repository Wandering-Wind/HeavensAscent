using UnityEngine;

public class ButtonClickAudio : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSound;
    public AudioClip Score_Sound;
    public AudioClip WinnerAAAH;

    public void PlayClick()
    {
        audioSource.PlayOneShot(clickSound);
    }

    public void PlayScore()
    {
        audioSource.PlayOneShot(Score_Sound);
    }
    public void PlayWinnerAAAH()
    {
        audioSource.PlayOneShot(WinnerAAAH);
    }
}
