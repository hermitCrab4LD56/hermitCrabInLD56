using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;

public class ButtonLogic : MonoBehaviour
{
    public EventSystem EventSystem { get; private set; }
    public FindHighlight highlightScript; // ȷ��������Highlight�ű�������

    public GameObject initialButton;
    public GameObject FirstSelectedGameObject;
    public GameObject trueselectedObject;



    void Start()
    {
        EventSystem = GetComponent<EventSystem>();
        

        // �ڿ�ʼʱ����Ϊָ���ĳ�ʼ��ť
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
