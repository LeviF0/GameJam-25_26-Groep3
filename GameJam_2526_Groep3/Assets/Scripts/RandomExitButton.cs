using UnityEngine;
using UnityEngine.UI;

public class RandomExitButton : MonoBehaviour
{
    public Button xButton; // Assign the scene button directly

    void Awake()
    {
        if (xButton != null)
            xButton.gameObject.SetActive(false); // Hide at start
    }

    public void ShowAtRandomLocation()
    {
        if (xButton == null) return;

        RectTransform rect = xButton.GetComponent<RectTransform>();
        // Random position using screen width and height
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        float halfWidth = screenWidth / 2 - rect.rect.width / 2;
        float halfHeight = screenHeight / 2 - rect.rect.height / 2;

        float randomX = Random.Range(-halfWidth, halfWidth);
        float randomY = Random.Range(-halfHeight, halfHeight);

        rect.anchoredPosition = new Vector2(randomX, randomY);
        xButton.gameObject.SetActive(true);
    }

    public void HideButton()
    {
        if (xButton != null)
            xButton.gameObject.SetActive(false);
    }
}
