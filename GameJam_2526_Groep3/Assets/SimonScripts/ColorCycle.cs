using TMPro;
using UnityEngine;

public class ColorCycle : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    public float switchInterval = 0.5f; 

    private char[] letters;
    private int[] colorState;       
    private float[] timers;        

    void Start()
    {
        if (textMesh == null)
        {
            Debug.LogError("TextMeshProUGUI not assigned!");
            return;
        }

        letters = textMesh.text.ToCharArray();
        colorState = new int[letters.Length];
        timers = new float[letters.Length];

        for (int i = 0; i < letters.Length; i++)
        {
            timers[i] = Random.Range(0f, switchInterval);
        }

        UpdateText();
    }

    void Update()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            timers[i] -= Time.deltaTime;
            if (timers[i] <= 0f)
            {
                colorState[i] = (colorState[i] + 1) % 3; 
                timers[i] = switchInterval;              
            }
        }

        UpdateText();
    }

    void UpdateText()
    {
        string display = "";
        for (int i = 0; i < letters.Length; i++)
        {
            string color = "white";
            if (colorState[i] == 1) color = "green";
            else if (colorState[i] == 2) color = "black";

            display += $"<color={color}>{letters[i]}</color>";
        }

        textMesh.text = display;
    }
}
