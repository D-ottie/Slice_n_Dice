using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenDelay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine(DelayMenu()); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DelayMenu()
    {
        Debug.Log("Coroutine called");
        yield return new WaitForSeconds(3);
        Debug.Log("Delay successful");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }
}
