using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
   public void ResetGame()
    {
        Debug.Log("LeviScene");
        SceneManager.LoadScene("LeviScene");
    }
    public void ExitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
