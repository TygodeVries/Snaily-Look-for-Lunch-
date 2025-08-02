using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    public GameObject TransitionFocus;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindAnyObjectByType<MyInputManager>().GoalReached();
        StartCoroutine(GoToNextLevel());
    }

    public IEnumerator GoToNextLevel()
    {
        GetComponentInParent<Animator>().SetTrigger("Reach");
        yield return new WaitForSeconds(2);
        Debug.Log("Next Level!");
        StartCoroutine(FindAnyObjectByType<Transition>().ExitAndLeave(nextLevelName));
    }
}
