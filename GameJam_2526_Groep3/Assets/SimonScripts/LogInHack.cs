using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogInHack : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI letterText;
    public TMP_InputField answerInput;
    public TextMeshProUGUI feedbackText;
    public TextMeshProUGUI attemptsText;

    public float fadeSpeed = 2f;
    private Color originalColor;

    [Header("Word Settings")]
    public List<string> wordList;

    public string chosenWord;
    private string scrambledWord;
    public int attempts = 3;

    [SerializeField] Image hackTab;
    private BouncingUi bouncingUi;

    void Start()
    {
        PickAndScrambleWord();
        feedbackText.text = "";
    }

    private void Update()
    {
        attemptsText.text = attempts.ToString();

        if (letterText != null)
        {
            float alpha = Mathf.PingPong(Time.time * fadeSpeed, 1f);
            Color c = Color.green;
            c.a = alpha;
            letterText.color = c;
        }

        if (attempts <= 0)
        {
            feedbackText.text = "Game Over";
            // Game Over
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                CheckAnswer();
            }
        }
    }

    void PickAndScrambleWord()
    {
        int randomIndex = Random.Range(0, wordList.Count);
        chosenWord = wordList[randomIndex].ToUpper();

        scrambledWord = ShuffleWord(chosenWord);

        letterText.text = AddSpaces(scrambledWord);

        answerInput.text = "";
        feedbackText.text = "";
    }

    string ShuffleWord(string word)
    {
        char[] letters = word.ToCharArray();

        for (int i = 0; i < letters.Length; i++)
        {
            int randomIndex = Random.Range(0, letters.Length);
            // swap
            char temp = letters[i];
            letters[i] = letters[randomIndex];
            letters[randomIndex] = temp;
        }

        return new string(letters);
    }

    string AddSpaces(string word)
    {
        return string.Join(" ", word.ToCharArray());
    }

    public void CheckAnswer()
    {
        string playerAnswer = answerInput.text.ToUpper();

        if (playerAnswer == chosenWord)
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;
            // Mag weer in Netflix
        }
        else
        {
            attempts--;
            bouncingUi = FindFirstObjectByType<BouncingUi>();
            bouncingUi.speed += new Vector2(100f, 80f);
            feedbackText.text = "Wrong!";
            feedbackText.color = Color.red;
        }
    }

    public void RefreshWord()
    {
        PickAndScrambleWord();
        if (bouncingUi != null)
        {
            bouncingUi.speed = new Vector2(200f, 150f);
        }
        attempts = 3;
    }

    public void HackButton()
    {
        hackTab.gameObject.SetActive(true);
        if (letterText != null)
            originalColor = letterText.color;
    }
}
