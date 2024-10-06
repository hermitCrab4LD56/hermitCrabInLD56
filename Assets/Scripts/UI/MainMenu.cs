using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    public GameObject rulePanel;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject GetHighlightedButton()
    {
        var standaloneInputModule = EventSystem.current.currentInputModule as StandaloneInputModule;
        if (standaloneInputModule != null)
        {
            FieldInfo fieldInfo = typeof(StandaloneInputModule).GetField("m_CurrentFocusedGameObject", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fieldInfo != null)
            {
                return fieldInfo.GetValue(standaloneInputModule) as GameObject;
            }
        }
        return null;
    }
    void Update()
    {
        GameObject highlightedButton = GetHighlightedButton();
        if (highlightedButton != null)
        {
            Debug.Log("Currently highlighted button: " + highlightedButton.name);
        }
    }

    public void Rule()
    {
        rulePanel.SetActive(true);
    }

    public void CloseRulePanel()
    {
        rulePanel.SetActive(false);
    }

    public void credit()
    {
        credits.SetActive(true);
    }
    public void creditclose()
    {
        credits.SetActive(false);
    }
    public void start()
    {

    }
}
