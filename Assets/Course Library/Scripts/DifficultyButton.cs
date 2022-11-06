using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private Button button;
    public int difficulty;
    
    // Start is called before the first frame update
    void Start()
    {

        button = GetComponent<Button>();
        button.onClick.AddListener(SetDiffculty);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetDiffculty()
    {
        Debug.Log(gameObject.name + " was clicked");
        FindObjectOfType<GameManager>().StartGame(difficulty);
    }
}
