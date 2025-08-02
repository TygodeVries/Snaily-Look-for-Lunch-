using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditText : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(50f);
        GetComponent<TMP_Text>().text = "A game by:\r\nTygo de vries\r\nRichard de vries";

        yield return new WaitForSeconds(5);
        GetComponent<TMP_Text>().text = "THANK YOU \r\nFOR PLAYING";

        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    }
}
