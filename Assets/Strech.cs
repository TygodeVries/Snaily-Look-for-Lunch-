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

        

        transform.GetChild(0).localScale = stretch;
    }
}
