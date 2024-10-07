using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public Button yourButton; 
    public int sceneIndex; 

    void Start()
    {
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(() => LoadScene(sceneIndex)); 
        }
    }

    void LoadScene(int index)
    {
       
        SceneManager.LoadScene(index);
    }
}
