using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip buttonClickClip; 
    public AudioClip buttonHoverClip; 


    public void PlayButtonClickSound()
    {
        if (audioSource != null && buttonClickClip != null)
        {
            audioSource.PlayOneShot(buttonClickClip); 
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is not assigned!");
        }
    }

    public void PlayButtonHoverSound()
    {
        if (audioSource != null && buttonHoverClip != null)
        {
            audioSource.PlayOneShot(buttonHoverClip); 
        }
        else
        {
            Debug.LogError("AudioSource or AudioClip is not assigned!");
        }
    }

}
