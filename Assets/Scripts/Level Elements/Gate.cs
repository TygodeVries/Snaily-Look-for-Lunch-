using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;

public class Gate : MonoBehaviour
{
    [SerializeField] private float OpenLocationOffset;
    [SerializeField] private float Speed = 10;


    private Vector2 openPosistion;
    private Vector2 closePosistion;
    private void Start()
    {
        closePosistion = transform.position;
        openPosistion = transform.position + new Vector3(0, OpenLocationOffset);
    }

    public UnityEvent testEvent;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + new Vector3(0, OpenLocationOffset), transform.localScale);
    }


    public bool isOpen = false;
    public void Open()
    {
        isOpen = true;
    }

    public void Close()
    {
        isOpen = false;
    }
    public void Update()
    {
        if (isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, openPosistion, Time.deltaTime * Speed);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, closePosistion, Time.deltaTime * Speed);
        }
    }
}
