using UnityEngine;

public class Strech : MonoBehaviour
{
    Rigidbody2D body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 stretch = new Vector2(1, 1);

        if(body.linearVelocityY < 0)
            stretch.x = 1 - Mathf.Clamp(Mathf.Abs(body.linearVelocityY) / 20, 0f, 0.7f);
        

        transform.GetChild(0).localScale = stretch;
    }
}
