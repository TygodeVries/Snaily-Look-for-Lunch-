using UnityEngine;

public class InstructionSwitch : MonoBehaviour
{
    public GameObject walkInstr;
    public GameObject jumpInstr;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        walkInstr.SetActive(false);
        jumpInstr.SetActive(true);
    }
}
