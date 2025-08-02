using UnityEngine;

public class Booster : MonoBehaviour
{
    public Vector2 velocity;

    public void Boost()
    {
        GetComponent<Rigidbody2D>().linearVelocity = velocity;
    }
}
