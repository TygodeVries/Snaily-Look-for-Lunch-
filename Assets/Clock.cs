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
            float amount = -((timerUI.time_left / 10f) * 2f) + 0.5f;
            Vector2 rot = new Vector2(Mathf.Cos(amount * Mathf.PI) * size, Mathf.Sin(amount * Mathf.PI) * size);
            transform.GetChild(0).localPosition = rot;
        }
    }
}
