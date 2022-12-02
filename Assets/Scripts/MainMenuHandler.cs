using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
//     public GameObject homeScreen;
//     public GameObject titleScreen;


    void Start()
    {
        // Debug.Log("Scene started");
        // StartCoroutine(TitleScreenDelay());
        // Debug.Log("Already called Coroutine man!");
        
    }

    // IEnumerator TitleScreenDelay()
    // {
    //     Debug.Log("Coroutine called");
    //     yield return new WaitForSeconds(3);
    //     Debug.Log("Delay successful");
    //     homeScreen.SetActive(true);
    //     titleScreen.SetActive(false);

    // }

    public void LoadOtherScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
