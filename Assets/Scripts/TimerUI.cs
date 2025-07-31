using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public GameObject pos_ref;
    MyInputManager mip;
    Camera cam;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
    {
		mip = GetComponentInParent<MyInputManager>();
		cam = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update()
    {
        float p = (float)(mip.time_left / mip.round_time);


        Vector3 pos = pos_ref.transform.position;

        LineRenderer tr = GetComponent<LineRenderer>();
        Vector3[] positions = new Vector3[100];
        if (p < 0.5)
        {
            tr.positionCount = (int)(200 * p);
            for (int cx = 0; cx < 200 * p; cx++)
            {
                float t = 2.0f * 3.141592653f * (float)cx / 100.0f;
                positions[cx].x = 2.0f * Mathf.Sin(t);
                positions[cx].y = 0.5f * Mathf.Sin(2.0f * t);
                positions[cx].z = 1;
                positions[cx] += pos;
            }
        }
        else
        {
            tr.positionCount = (int)(200 * (1 - p));
            for (int cx = 0; cx < 200 * (1 - p); cx++)
            {
                float t = 2.0f * 3.141592653f * (float)(cx + 200 * p) / 100.0f;
                positions[cx].x = 2.0f * Mathf.Sin(t);
                positions[cx].y = 0.5f * Mathf.Sin(2.0f * t);
                positions[cx].z = 1;
                positions[cx] += pos;
            }
        }
		tr.SetPositions(positions);

    }
}
