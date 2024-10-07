using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAndHighlightLogic : MonoBehaviour
{
    public EventSystem EventSystem { get; private set; }
    public GameObject LastHighlightedButton { get; private set; }

    public GameObject currentHoveredButton;
    public GameObject currentHovered;
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
        }
    }

    void Update()
    {
        CheckMouseHover();

        GameObject selectedObject = LastHighlightedButton;

        // ��������Ķ���ͬ�ڵ�ǰѡ�еĶ��������ѡ�еĶ���
        if (selectedObject != null && selectedObject != FirstSelectedGameObject)
        {
            FirstSelectedGameObject = selectedObject;
            EventSystem.SetSelectedGameObject(FirstSelectedGameObject);
        }

        trueselectedObject = EventSystem.current.currentSelectedGameObject;

        if (currentHoveredButton == trueselectedObject && Input.GetMouseButtonDown(0))
        {
            Button button = currentHoveredButton.GetComponent<Button>();
            button?.onClick.Invoke(); 
        }
    }

    private void CheckMouseHover()
    {
        Vector2 mousePosition = Input.mousePosition;

        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        // Ĭ�϶�����Ϊ null
        LastHighlightedButton = null;
        currentHoveredButton = null;
        currentHovered = null;
        
        if (results.Count > 0)
        {
            GameObject hoveredObject = results[0].gameObject;
            currentHoveredButton = hoveredObject.GetComponent<Button>() != null ? hoveredObject : null;

            if (currentHoveredButton != null && currentHoveredButton != LastHighlightedButton)
            {
                LastHighlightedButton = currentHoveredButton;
            }
        }
        

        if (results.Count > 0)
        {
            GameObject hoveredObject = results[0].gameObject;
            currentHovered = hoveredObject; // ֱ�ӻ�ȡ��ͣ�Ķ���

        }
    }
}
