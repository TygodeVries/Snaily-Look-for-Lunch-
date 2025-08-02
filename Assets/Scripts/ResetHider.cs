using UnityEngine;

public class ResetHider : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        FindAnyObjectByType<MyInputManager>().NewRoundEvent.AddListener(this.NewRound);
        GetComponent<SpriteRenderer>().enabled = false;
	}
	void NewRound(float start_time, int player_count)
    {
        GetComponent<SpriteRenderer>().enabled = (player_count > 5);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
