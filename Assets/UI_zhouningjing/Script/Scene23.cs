using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scenebacktomain : MonoBehaviour
{
    public Button Button; 
    public int scIndex; 
    public Image transitionImage; 
    public float transitionTime = 0.1f; 
    public float stayDuration = 0.2f; 
    

    void Start()
    {
        if (Button != null)
        {
            Button.onClick.AddListener(() => StartCoroutine(LoadSceneWithTransition(scIndex)));
        }

        if (transitionImage != null)
        {
            Color color = transitionImage.color;
            color.a = 0; 
            transitionImage.color = color;
            transitionImage.gameObject.SetActive(false); 
        }
    }

    private IEnumerator LoadSceneWithTransition(int index)
    {
        
        yield return new WaitForSeconds(stayDuration);

        if (transitionImage != null)
        {
            transitionImage.gameObject.SetActive(true); 
            yield return FadeIn();
        }

        SceneManager.LoadScene(index);
    }

    private IEnumerator FadeIn()
    {
        Color color = transitionImage.color;
        float startAlpha = color.a;

        for (float t = 0; t < transitionTime; t += Time.deltaTime)
        {
            float normalizedTime = t / transitionTime;
            color.a = Mathf.Lerp(startAlpha, 1, normalizedTime);
            transitionImage.color = color;
            yield return null;
        }
        color.a = 1;
        transitionImage.color = color;
    }
}
