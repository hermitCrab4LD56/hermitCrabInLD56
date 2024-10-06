using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject rulePanel;
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
