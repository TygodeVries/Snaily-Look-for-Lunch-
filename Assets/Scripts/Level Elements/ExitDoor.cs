using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private string nextLevelName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GoToNextLevel();
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(nextLevelName);
    }
}
