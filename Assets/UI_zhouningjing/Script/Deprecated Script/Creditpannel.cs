using UnityEngine;
using UnityEngine.UI;

public class Creditpannel : MonoBehaviour
{
    public GameObject creditpanel; 
    public Button CREDITbutton;
    public Button exitbutton_2;
    private bool isCREDITOpen = false; 

    void Start()
    {

        if (CREDITbutton != null)
        {
            CREDITbutton.onClick.AddListener(OpenPanel_2);
        }

        if (exitbutton_2 != null)
        {
            exitbutton_2.onClick.AddListener(closePanel_2);
        }
    }

    void OpenPanel_2()
    {
        if (creditpanel != null && !isCREDITOpen) 
        {
            creditpanel.SetActive(true);
            isCREDITOpen = true;
        }
    }

    void closePanel_2()
    {
        if (creditpanel != null && isCREDITOpen)
        {
            creditpanel.SetActive(false);
            isCREDITOpen = false;
        }
    }
}