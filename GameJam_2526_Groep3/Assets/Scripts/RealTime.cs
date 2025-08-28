using UnityEngine;
using UnityEngine.UI;

public class RealTime : MonoBehaviour
{
    [SerializeField] Text time;
    private void Update()
    {
        System.DateTime myTime = System.DateTime.Now;
        time.text = $"{myTime.Hour}:{myTime.Minute}";
    }
}
