using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReactionGame : MonoBehaviour
{
    public Image targetImage;           // The UI Image component to display sprites
    public Sprite[] sprites;            // 5 sprites (good + bad)
    public int badSpriteIndex = 0;      // Which one is the "bad" sprite (set in Inspector)
    public KeyCode reactionKey = KeyCode.Space;
    public float maxReactionTime = 2f;

    private bool isImageActive = false;
    private bool isBadImage = false;
    private float reactionTimer = 0f;

    void Start()
    {
        targetImage.gameObject.SetActive(false);
        StartCoroutine(GameLoop());
    }

    void Update()
    {
        if (isImageActive)
        {
            reactionTimer -= Time.deltaTime;

            if (Input.GetKeyDown(reactionKey))
            {
                if (isBadImage)
                {
                    Success();
                }
                else
                {
                    Fail("Pressed on a good image!");
                }
            }
            else if (reactionTimer <= 0f)
            {
                if (isBadImage)
                    Fail("Did not press on the bad image!");
                else
                    Success(); // Correctly ignored a good image
            }
        }
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            // Wait a random time before showing an image
            float waitTime = Random.Range(1f, 3f);
            yield return new WaitForSeconds(waitTime);

            // Pick a random sprite
            int index = Random.Range(0, sprites.Length);
            targetImage.sprite = sprites[index];
            isBadImage = (index == badSpriteIndex);

            // Show image
            targetImage.gameObject.SetActive(true);
            isImageActive = true;
            reactionTimer = maxReactionTime;

            // Wait until Success() or Fail() ends this round
            while (isImageActive)
                yield return null;
        }
    }

    void Success()
    {
        Debug.Log("Success!");
        targetImage.gameObject.SetActive(false);
        isImageActive = false;
    }

    void Fail(string reason)
    {
        Debug.Log("Fail: " + reason);
        targetImage.gameObject.SetActive(false);
        isImageActive = false;
    }
}
