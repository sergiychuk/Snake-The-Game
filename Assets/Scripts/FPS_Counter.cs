using UnityEngine.UI;
using UnityEngine;

public class FPS_Counter : MonoBehaviour
{

    public Text fps_text;
    float currentFPS = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentFPS = (int)(1f / Time.unscaledDeltaTime);
        fps_text.text = "FPS:";
    }

    // Update is called once per frame
    void Update()
    {
        currentFPS = (int)(1f / Time.unscaledDeltaTime);
        fps_text.text = "FPS: " + currentFPS.ToString();
    }
}
