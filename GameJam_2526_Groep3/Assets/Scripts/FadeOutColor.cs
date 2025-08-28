using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FadeOutColor : MonoBehaviour
{
    [SerializeField] Image image;




    private void Start()
    {
        StartCoroutine(TransitionColorAnimation());
    }

    IEnumerator TransitionColorAnimation()
    {

        // Duration of the fade-out effect
        float duration = 3.0f; // Adjust as needed
        float elapsedTime = 0f;

        // Gradually reduce the alpha value
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            image.color = new Color(0f, 0f, 0f, alpha);

            // Apply the updated color to the object (if applicable)
            if (TryGetComponent<Renderer>(out Renderer renderer))
            {
                renderer.material.color = image.color;
            }

            yield return null;
        }

        Destroy(this.gameObject);
    }
}

