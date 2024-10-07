using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; 

public class Backgaudiomanager : MonoBehaviour
{
    public static Backgaudiomanager Instance { get; private set; }
    public AudioSource audioSource; 
    public AudioClip[] backgroundMusic; 
    public float fadeDuration = 1f; // ½¥±äÊ±³¤

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBackgroundMusic(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBackgroundMusic(scene.buildIndex);
    }

    public void PlayBackgroundMusic(int sceneIndex)
    {
        if (audioSource != null && backgroundMusic.Length > sceneIndex && backgroundMusic[sceneIndex] != null)
        {
            if (audioSource.clip != backgroundMusic[sceneIndex])
            {
                //StartCoroutine(FadeOutCurrentMusic()); 
                audioSource.clip = backgroundMusic[sceneIndex];
                StartCoroutine(FadeInBackgroundMusic()); 
            }
        }
        else
        {
            Debug.LogError("AudioSource or BackgroundMusic is not assigned, or index is out of range!");
        }
    }

    private IEnumerator FadeOutCurrentMusic()
    {
        float startVolume = audioSource.volume;
        yield return null;
        //while (audioSource.volume > 0)
        //{
        //    audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
        //    yield return null;
        //}

        audioSource.Stop();
        audioSource.volume = startVolume; 
    }

    private IEnumerator FadeInBackgroundMusic()
    {
        audioSource.volume = 0; 
        audioSource.Play();
        yield return null;
        //while (audioSource.volume < 1)
        //{
        //    audioSource.volume += Time.deltaTime / fadeDuration; 
        //    yield return null;
        //}

        audioSource.volume = 1; 
    }

    public void LoadNextScene()
    {
        int nextSceneIndex = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
