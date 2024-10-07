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

        // 在开始时设置为指定的初始按钮
        if (initialButton != null && initialButton.GetComponent<Button>() != null)
        {
            EventSystem.SetSelectedGameObject(initialButton);
        }
    }

    void Update()
    {
        CheckMouseHover();

        GameObject selectedObject = LastHighlightedButton;

        // 如果高亮的对象不同于当前选中的对象，则更新选中的对象
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

        // 默认都设置为 null
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
            currentHovered = hoveredObject; // 直接获取悬停的对象

        }
    }
}
