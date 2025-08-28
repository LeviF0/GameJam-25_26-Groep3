using UnityEngine;
using UnityEngine.UI;

public class DopamineFillScript : MonoBehaviour
{
    public Image Dopamine;
    public float fillDuration = 10f;

    public Button toggleButton;

    private float fillTimer = 0f;
    private bool isFilling = false;

    void Start()
    {
        Dopamine.fillAmount = 0f;
        toggleButton.onClick.AddListener(ToggleFilling);
    }

    void Update()
    {
        if (isFilling)
        {
            if (fillTimer < fillDuration)
            {
                fillTimer += Time.deltaTime / 2f;
                Dopamine.fillAmount = (fillTimer / fillDuration);
            }
        }
        else
        {
            if (fillTimer > 0f)
            {
                fillTimer -= Time.deltaTime / 4f;
                Dopamine.fillAmount = (fillTimer / fillDuration);
            }
        }
    }

    public void ToggleFilling()
    {
        isFilling = !isFilling;
    }
}
