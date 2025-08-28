using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExPopupScript : MonoBehaviour
{
    public Image targetImage;
    public Image netflixImage;
    public Button resumeButton;
    public Sprite[] sprites;
    public AudioSource sfxSource;
    public AudioClip imageSfx;

    public RandomExitButton randomExitButton;
    public DopamineFillScript dopamineFillScript; // Assign in Inspector

    private int badSpriteIndex = 0;
    private float maxReactionTime = 2f;
    private bool isPopupActive = false;
    private bool isBadImage = false;
    private float reactionTimer = 0f;
    private bool isPaused = true;

    void Start()
    {
        targetImage.gameObject.SetActive(false);
        netflixImage.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        resumeButton.onClick.AddListener(ResumeGame);

        if (randomExitButton?.xButton != null)
            randomExitButton.xButton.onClick.AddListener(OnXButtonPressed);

        StartCoroutine(GameLoop());
    }

    void Update()
    {
        // Only control fill/drain state here, not the actual progress
        if (dopamineFillScript != null)
        {
            if (netflixImage.gameObject.activeSelf)
                dopamineFillScript.StartFilling();
            else
                dopamineFillScript.StartDraining();
        }

        if (isPaused || !isPopupActive) return;

        reactionTimer -= Time.deltaTime;
        if (reactionTimer <= 0f)
        {
            if (isBadImage)
            {
                Fail("Did not press the bad image in time");
            }
            else
            {
                ClearPopup();
            }
        }
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            if (isPaused)
            {
                yield return null;
                continue;
            }

            yield return new WaitForSeconds(Random.Range(1f, 5f));
            if (isPaused) continue;

            int index = Random.Range(0, sprites.Length);
            targetImage.sprite = sprites[index];
            isBadImage = (index == badSpriteIndex);

            targetImage.gameObject.SetActive(true);
            isPopupActive = true;
            reactionTimer = maxReactionTime;

            if (sfxSource && imageSfx)
                sfxSource.PlayOneShot(imageSfx);

            randomExitButton?.ShowAtRandomLocation();

            while (isPopupActive && !isPaused)
                yield return null;
        }
    }

    void OnXButtonPressed()
    {
        if (!isPopupActive) return;

        if (isBadImage)
        {
            Success();
        }
        else
        {
            Fail("Pressed on a good image!");
        }
    }

    void Success()
    {
        ClearPopup();
        ClearScreen();
        isPaused = true;
    }

    void Fail(string reason)
    {
        Debug.Log("Fail: " + reason);
        dopamineFillScript?.DecreaseBar(0.1f); // Lose 10%
        ClearPopup();
        ClearScreen();
        isPaused = true;
    }

    void ClearScreen()
    {
        netflixImage.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
    }

    void ClearPopup()
    {
        targetImage.gameObject.SetActive(false);
        isPopupActive = false;
        randomExitButton?.HideButton();
    }

    void ResumeGame()
    {
        netflixImage.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        isPaused = false;
    }
}
