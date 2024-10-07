using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonAudioManager : MonoBehaviour
{
    public AudioManager audioManager;
    public Button[] buttons;
    public float scaleMultiplier = 1.5f;
    private Button lastSelectedButton;

    private void Start()
    {
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(OnButtonClick);
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            EventTrigger.Entry entrySelect = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Select
            };
            entrySelect.callback.AddListener((eventData) => { OnButtonSelect(button); });
            trigger.triggers.Add(entrySelect);

            EventTrigger.Entry entryDeselect = new EventTrigger.Entry
            {
                eventID = EventTriggerType.Deselect
            };
            entryDeselect.callback.AddListener((eventData) => { OnButtonDeselect(button); });
            trigger.triggers.Add(entryDeselect);
        }
    }

    private void OnButtonClick()
    {
        audioManager.PlayButtonClickSound();
    }

    private void OnButtonSelect(Button button)
    {
        audioManager.PlayButtonHoverSound();
        if (lastSelectedButton != null)
        {
            lastSelectedButton.transform.localScale /= scaleMultiplier;
            StopCoroutine("PulseEffect");
        }
        lastSelectedButton = button;
        button.transform.localScale *= scaleMultiplier;
        StartCoroutine("PulseEffect", button);
    }

    private void OnButtonDeselect(Button button)
    {
        if (button == lastSelectedButton)
        {
            button.transform.localScale /= scaleMultiplier;
            lastSelectedButton = null;
            StopCoroutine("PulseEffect");
            SetButtonAlpha(button, 1f);
        }
    }

    private IEnumerator PulseEffect(Button button)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            float duration = 0.6f; //Âö¶¯Ê±³¤
            float elapsedTime = 0f;

            while (true)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.PingPong(elapsedTime / duration, 1f);
                SetButtonAlpha(button, alpha);
                yield return null;
            }
        }
    }

    private void SetButtonAlpha(Button button, float alpha)
    {
        Image buttonImage = button.GetComponent<Image>();
        if (buttonImage != null)
        {
            Color color = buttonImage.color;
            color.a = alpha;
            buttonImage.color = color;
        }
    }
}