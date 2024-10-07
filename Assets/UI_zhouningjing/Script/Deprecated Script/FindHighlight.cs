using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FindHighlight : MonoBehaviour
{
    public GameObject LastHighlightedButton { get; private set; }
    public GameObject currentHoveredButton;

   
    void Update()
    {
        CheckMouseHover();
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

        if (results.Count > 0)
        {
            GameObject hoveredObject = results[0].gameObject;
            currentHoveredButton = hoveredObject.GetComponent<Button>() != null ? hoveredObject : null;

            if (currentHoveredButton != null && currentHoveredButton != LastHighlightedButton)
            {
                LastHighlightedButton = currentHoveredButton;
               // Debug.Log("Last Highlighted Button: " + LastHighlightedButton.name);
            }
        }
    }
}
