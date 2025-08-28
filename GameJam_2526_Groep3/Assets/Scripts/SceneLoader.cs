using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Call this method to load the "LosingScene"
    public void LoadLosingScene()
    {
        SceneManager.LoadScene("LosingScene");
    }

    public void LoadWinScene()
    {
        SceneManager.LoadScene("WinScene");
    }
}
