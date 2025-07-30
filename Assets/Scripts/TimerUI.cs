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

        Camera cam = GetComponent<Camera>();

        Vector3 pos = cam.ScreenToWorldPoint(new Vector3(-0.5f, 0.5f, 0));

        LineRenderer tr = GetComponent<LineRenderer>();
        tr.positionCount = (int)(100 * p);
        Vector3[] positions = new Vector3[100];
        for (int cx = 0; cx < 100 * p; cx++)
        {
            float t = 2.0f * 3.141592653f * (float)cx / 100.0f;
            positions[cx].x = 2.5f + 2.0f * Mathf.Sin(t);
            positions[cx].y = Mathf.Sin(2.0f * t) + 1.5f;
            positions[cx] += pos;
        }
        tr.SetPositions(positions);

    }
}
