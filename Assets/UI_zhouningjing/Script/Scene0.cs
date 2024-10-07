using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scene0 : MonoBehaviour
{
    public Button Button;
    public int scIndex; 
    public Image transitionImage;
    public RectTransform panelToScale; 
    public float transitionTime = 1f; 
    public float stayDuration = 1f; 
    public float scaleDuration = 1f; 

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

       
        if (panelToScale != null)
        {
            yield return ScalePanel(panelToScale, scaleDuration, 2f); 
        }

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
            //transitionImage.color = color;
            yield return null;
        }
        color.a = 1;
        transitionImage.color = color;
    }

    private IEnumerator ScalePanel(RectTransform panel, float duration, float targetScale)
    {
        Vector3 initialScale = panel.localScale;
        Vector3 finalScale = initialScale * targetScale;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            float normalizedTime = t / duration;
            panel.localScale = Vector3.Lerp(initialScale, finalScale, normalizedTime);
            yield return null;
        }

        panel.localScale = finalScale;
    }
}
