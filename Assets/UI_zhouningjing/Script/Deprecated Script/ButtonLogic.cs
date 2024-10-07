using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ButtonLogic : MonoBehaviour
{
    public EventSystem EventSystem { get; private set; }
    public FindHighlight highlightScript; // 确保这里是Highlight脚本的引用

    public GameObject initialButton;
    public GameObject FirstSelectedGameObject;
    public GameObject trueselectedObject;



    void Start()
    {
        EventSystem = GetComponent<EventSystem>();
        

        // 在开始时设置为指定的初始按钮
        if (initialButton != null && initialButton.GetComponent<Button>() != null)
        {
            EventSystem.SetSelectedGameObject(initialButton);
            //Debug.Log("Initial Highlighted Button set to: " + highlightScript.LastHighlightedButton.name);
        }
    }

    void Update()
    {
        GameObject selectedObject = highlightScript.LastHighlightedButton;
        //GameObject selectedObject2 = EventSystem.current.currentSelectedGameObject;

        if (selectedObject != null && selectedObject != FirstSelectedGameObject)
        {
            FirstSelectedGameObject = selectedObject;
            EventSystem.SetSelectedGameObject(FirstSelectedGameObject);
            //Debug.Log("First Selected GameObject set to: " + FirstSelectedGameObject.name);
        }

        trueselectedObject = EventSystem.current.currentSelectedGameObject;
    }
}
