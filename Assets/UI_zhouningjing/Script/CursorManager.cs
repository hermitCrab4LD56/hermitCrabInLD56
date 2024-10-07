using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor; 
    public Texture2D hoverCursor; 
    public Vector2 hotSpot = Vector2.zero;

    void Start()
    {
        
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
    }

    public void SetHoverCursor()
    {
        
        Cursor.SetCursor(hoverCursor, hotSpot, CursorMode.Auto);
    }

    public void ResetCursor()
    {
        
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
    }
}
