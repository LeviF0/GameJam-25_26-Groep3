using UnityEngine;
using UnityEngine.UI;

public class RandomExitButton : MonoBehaviour
{
    public GameObject xButtonPrefab;
    public Canvas canvas;
    private GameObject currentButton;

    public void RandomLocation()
    {
        Destroy(currentButton);
        currentButton = Instantiate(xButtonPrefab, canvas.transform);

        RectTransform rect = currentButton.GetComponent<RectTransform>();
        float randomX = Random.Range(-960f, 960f);
        float randomY = Random.Range(-540f, 540f);
        rect.anchoredPosition = new Vector2(randomX, randomY);

        Button button = currentButton.GetComponent<Button>();
    }

    public void OnXbuttonClick()
    {
        Destroy(currentButton);
        currentButton = null;
    }
}
