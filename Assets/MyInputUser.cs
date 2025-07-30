using UnityEngine;

public class MyInputUser : MonoBehaviour
{
    public int player_id;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jump()
    {
        Debug.Log($"{player_id} :: Jump");
    }
    public void StartRight()
    {
    }
    public void StopRight()
    {
    }
    public void StartLeft()
    {
    }
    public void StopLeft()
    {
    }
}
