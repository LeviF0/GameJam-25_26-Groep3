using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExPopupScript : MonoBehaviour
{
    public Image targetImage;
    public Image netflixImage;
    public Button reactionButton;
    public Button resumeButton;
    public Sprite[] sprites;
    public AudioSource sfxSource;
    public AudioClip imageSfx;

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
        reactionButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);

        reactionButton.onClick.AddListener(OnButtonPressed);
        resumeButton.onClick.AddListener(ResumeGame);

        StartCoroutine(GameLoop());
    }

    void Update()
    {
        if (isPaused) return;

        if (isPopupActive)
        {
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

            float waitTime = Random.Range(1f, 5f);
            yield return new WaitForSeconds(waitTime);

            if (isPaused) continue;

            int index = Random.Range(0, sprites.Length);
            targetImage.sprite = sprites[index];
            isBadImage = (index == badSpriteIndex);

            targetImage.gameObject.SetActive(true);
            isPopupActive = true;
            reactionTimer = maxReactionTime;

            if (sfxSource != null && imageSfx != null)
                sfxSource.PlayOneShot(imageSfx);

            while (isPopupActive && !isPaused)
                yield return null;
        }
    }

    void OnButtonPressed()
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

        reactionButton.gameObject.SetActive(false);
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
        ClearPopup();
        ClearScreen();
        isPaused = true;
    }

    void ClearScreen()
    {
        netflixImage.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        reactionButton.gameObject.SetActive(false);
    }

    void ClearPopup()
    {
        targetImage.gameObject.SetActive(false);
        isPopupActive = false;
    }

    void ResumeGame()
    {
        reactionButton.gameObject.SetActive(true);
        netflixImage.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        isPaused = false;
    }
}
