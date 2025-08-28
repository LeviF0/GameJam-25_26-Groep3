using UnityEngine;

public class BouncingUi : MonoBehaviour
{
    public Vector2 speed = new Vector2(200f, 150f);
    private RectTransform rectTransform;
    private Canvas canvas;
    private Vector2 direction = new Vector2(1, 1);

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        if (canvas == null) Debug.LogError("No Canvas found in parent!");
    }

    void Update()
    {
        Vector2 pos = rectTransform.anchoredPosition;
        pos += direction * speed * Time.deltaTime;

        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        float halfWidth = rectTransform.rect.width * rectTransform.localScale.x / 2f;
        float halfHeight = rectTransform.rect.height * rectTransform.localScale.y / 2f;

        // Stuiter horizontaal
        if (pos.x + halfWidth > canvasWidth / 2f || pos.x - halfWidth < -canvasWidth / 2f)
        {
            direction.x *= -1;
            pos.x = Mathf.Clamp(pos.x, -canvasWidth / 2f + halfWidth, canvasWidth / 2f - halfWidth);
        }

        // Stuiter verticaal
        if (pos.y + halfHeight > canvasHeight / 2f || pos.y - halfHeight < -canvasHeight / 2f)
        {
            direction.y *= -1;
            pos.y = Mathf.Clamp(pos.y, -canvasHeight / 2f + halfHeight, canvasHeight / 2f - halfHeight);
        }

        rectTransform.anchoredPosition = pos;
    }

}
