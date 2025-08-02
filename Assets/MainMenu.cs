using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    void Update()
    {
        if(Input.anyKeyDown && !Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("Intro");
        }
    }
}
