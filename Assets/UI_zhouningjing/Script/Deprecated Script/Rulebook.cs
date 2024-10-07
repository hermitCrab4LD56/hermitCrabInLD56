using UnityEngine;
using UnityEngine.UI;

public class Rulebook : MonoBehaviour
{
    public GameObject panel; 
    public Button rulebookbutton;
    public Button exitbutton;
    private bool isPanelOpen = false; 

    void Start()
    {
        
      if (rulebookbutton != null)
      {
        rulebookbutton.onClick.AddListener(OpenPanel);
      }

      if(exitbutton !=null)
      {
            exitbutton.onClick.AddListener(closePanel);
        }
    }

    void OpenPanel()
    {
        if (panel != null && !isPanelOpen) 
        {
            panel.SetActive(true); 
            isPanelOpen = true;
        }
    }

    void closePanel ()
    {
        if (panel != null && isPanelOpen)
        {
            panel.SetActive(false);
            isPanelOpen = false;
        }
    }
}