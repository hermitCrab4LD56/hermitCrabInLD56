using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public GameObject targetImageObject; 
    public Sprite[] images; 
    private Image targetImage; 
    private int currentIndex = 0;

    private void Start()
    {
        
        targetImage = targetImageObject.GetComponent<Image>();
        UpdateImage(); // ��ʼ��ʱ��ʾ��һ��ͼƬ
    }

    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % images.Length; 
        UpdateImage();
    }

    public void PreviousImage()
    {
        currentIndex = (currentIndex - 1 + images.Length) % images.Length; 
        UpdateImage();
    }

    private void UpdateImage()
    {
        if (targetImage != null && images.Length > 0)
        {
            targetImage.sprite = images[currentIndex]; 
        }
    }
}
