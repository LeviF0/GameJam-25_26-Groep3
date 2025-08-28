using UnityEngine;

public class AppController : MonoBehaviour
{

    [SerializeField] GameObject app;
  public void Close()
    {
        app.SetActive(false);
    }

    public void Open()
    {
        app.SetActive(true);
    }
}
