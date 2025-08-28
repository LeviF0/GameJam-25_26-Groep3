using UnityEngine;
using UnityEngine.UI;

public class DopamineFillScript : MonoBehaviour
{
    public Image Dopamine;
    public float fillRate = 0.02f;  // Fill speed per second
    public float drainRate = 0.02f; // Drain speed per second

    private float fillAmount = 0f;
    private bool isFilling = false;
    private bool isDraining = false;

    void Start()
    {
        Dopamine.fillAmount = 0f;
    }

    void Update()
    {
        if (isFilling)
        {
            fillAmount += fillRate * Time.deltaTime;
        }
        else if (isDraining)
        {
            fillAmount -= drainRate * Time.deltaTime;
        }

        fillAmount = Mathf.Clamp01(fillAmount);
        Dopamine.fillAmount = fillAmount;
    }

    // Call to start filling
    public void StartFilling()
    {
        isFilling = true;
        isDraining = false;
    }

    // Call to start draining
    public void StartDraining()
    {
        isFilling = false;
        isDraining = true;
    }

    // Call to stop both filling and draining
    public void StopProgress()
    {
        isFilling = false;
        isDraining = false;
    }

    // Call this to decrease the bar by a percentage (0.1 = 10%)
    public void DecreaseBar(float percent)
    {
        fillAmount -= percent;
        fillAmount = Mathf.Clamp01(fillAmount);
        Dopamine.fillAmount = fillAmount;
    }
}
