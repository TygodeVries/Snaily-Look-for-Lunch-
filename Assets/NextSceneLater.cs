using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneLater : MonoBehaviour
{
    [SerializeField] private float After;
    [SerializeField] private string SceneName;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(After);
        SceneManager.LoadScene(SceneName);
    }
}
