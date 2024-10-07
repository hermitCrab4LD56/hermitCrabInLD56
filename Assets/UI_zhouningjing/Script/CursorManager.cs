using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D defaultCursor;
    public Texture2D hoverCursor;
    public Vector2 hotSpot = Vector2.zero;

    private bool isCursorVisible = true; // ����Ƿ�ɼ�
    private float hoverTime = 0f; // ��ͣʱ�����
    private const float hoverThreshold = 3f; // ��ͣ����3�����ֵ

    private void Awake()
    {
        // ȷ���ö����ڳ����л�ʱ��������
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
    }

    void Update()
    {
        // ������Ƿ�����Ϸ��Ļ��
        if (Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
            Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height)
        {
            hoverTime += Time.deltaTime; // ������ͣʱ��

            // ������ɼ�������ͣʱ�䳬����ֵ�����ع��
            if (hoverTime >= hoverThreshold && isCursorVisible)
            {
                HideAndLockCursor();
            }
        }
        else
        {
            ResetCursor(); // ���ù��
        }

        // �������ƶ��Իָ����
        if (!isCursorVisible && (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0))
        {
            ResetHoverTime(); // ȡ��������겢������ͣʱ��
        }
        else if (isCursorVisible)
        {
            // ������ɼ���������Ϸ��Ļ�ڣ�������ͣ���
            SetHoverCursor();
        }
    }

    public void SetHoverCursor()
    {
        Cursor.SetCursor(hoverCursor, hotSpot, CursorMode.Auto);
    }

    public void ResetCursor()
    {
        if (isCursorVisible) return; // �������ѿɼ�������Ҫ����

        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
        ResetHoverTime(); // ������ͣʱ��
    }

    private void HideAndLockCursor()
    {
        Cursor.visible = false; // ���ع��
        isCursorVisible = false; // �������״̬
    }

    private void ResetHoverTime()
    {
        hoverTime = 0f; // ������ͣʱ��
        if (!isCursorVisible)
        {
            Cursor.visible = true; // ȡ�����ع��
            isCursorVisible = true; // �������״̬
        }
    }
}
