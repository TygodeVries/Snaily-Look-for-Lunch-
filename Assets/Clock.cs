using UnityEngine;

public class Clock : MonoBehaviour
{
    public MyInputManager timerUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerUI = FindAnyObjectByType<MyInputManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        float size = 0.35f;

        if (timerUI.time_left > 0)
        {
            float amount = Mathf.PI * (timerUI.time_left / timerUI.round_time);
            transform.GetChild(0).rotation = new Quaternion(0, 0, Mathf.Sin(amount), Mathf.Cos(amount));
        }
    }
}
