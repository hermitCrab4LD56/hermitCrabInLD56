using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D hoverCursor;
    public Vector2 hotSpot = Vector2.zero;

    private bool isCursorVisible = true; // 光标是否可见
    private float hoverTime = 0f; // 悬停时间计数
    private const float hoverThreshold = 3f; // 悬停超过3秒的阈值

    private void Awake()
    {
        // 确保该对象在场景切换时不被销毁
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
    }

    void Update()
    {
        // 检测光标是否在游戏屏幕内
        if (Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
            Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height)
        {
            hoverTime += Time.deltaTime; // 增加悬停时间

            // 如果光标可见，且悬停时间超过阈值，隐藏光标
            if (hoverTime >= hoverThreshold && isCursorVisible)
            {
                HideAndLockCursor();
            }
        }
        else
        {
            ResetCursor(); // 重置光标
        }

        // 检测鼠标移动以恢复光标
        if (!isCursorVisible && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            ResetHoverTime(); // 取消锁定光标并重置悬停时间
        }
        else if (isCursorVisible)
        {
            // 如果光标可见，且在游戏屏幕内，更新悬停光标
            SetHoverCursor();
        }
    }

    public void SetHoverCursor()
    {
        Cursor.SetCursor(hoverCursor, hotSpot, CursorMode.Auto);
    }

    public void ResetCursor()
    {
        if (isCursorVisible) return; // 如果光标已可见，则不需要重置

        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
        ResetHoverTime(); // 重置悬停时间
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false; // 隐藏光标
        isCursorVisible = false; // 锁定光标状态
    }

    private void ResetHoverTime()
    {
        hoverTime = 0f; // 重置悬停时间
        if (!isCursorVisible)
        {
            Cursor.visible = true; // 取消隐藏光标
            isCursorVisible = true; // 解除锁定状态
        }
    }
}
