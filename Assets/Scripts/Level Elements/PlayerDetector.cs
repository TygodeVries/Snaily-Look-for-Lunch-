using UnityEngine;
using UnityEngine.Events;

public class PlayerDetector : MonoBehaviour
{
    public Sprite defaultSprite;
    public Sprite pressedSprite;
    int player_count = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public UnityEvent playerEnter;
    public UnityEvent playerExit;

    void Start()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (null != collision.gameObject.GetComponent<MyInputUser>())
        {
            if (0 == player_count)
            {
                GetComponent<SpriteRenderer>().sprite = pressedSprite;
                playerEnter.Invoke();
            }
			player_count++;
        }
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
        if (null != collision.gameObject.GetComponent<MyInputUser>())
        {
            if (player_count > 0)
            {
                player_count--;
                if (0 == player_count)
                {
                    GetComponent<SpriteRenderer>().sprite = defaultSprite;
                    playerExit.Invoke();
                }
            }
        }
	}
    public float compression_time = 1.0f;
    public float compression = 1.0f;
	// Update is called once per frame
	void Update()
    {
        if (player_count > 0 && compression > 0)
        {
            compression = Mathf.Max(0, compression - Time.deltaTime / compression_time);
        }
        if (player_count == 0 && compression < 1)
        {
            compression = Mathf.Min(1, compression + Time.deltaTime / compression_time);
        }
    }
}
