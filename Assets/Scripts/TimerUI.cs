using UnityEngine;

public class TimerUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MyInputManager mip = GetComponentInParent<MyInputManager>();
        float p = (float)(mip.time_left / mip.round_time);

        TrailRenderer tr = GetComponent<TrailRenderer>();

        if (p >= 0.9f)
        {
            tr.Clear();
            
        }
    }
}
