using UnityEngine;
using UnityEngine.UI;

public class FullScreenToggle : MonoBehaviour
{
    public Button fullScreenButton;

    void Start()
    {
        fullScreenButton.onClick.AddListener(ToggleFullScreen);
    }

    void ToggleFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen; 
    }

}
