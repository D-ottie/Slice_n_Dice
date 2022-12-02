using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    // public List<GameObject> clockPrefab;

    private float spawnRate = 1f;
    // public int countdown = 60;
    public int lives;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    //public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreTextOnGameOver;
    public TextMeshProUGUI bestScoreOnGameOver;
    public TextMeshProUGUI pointsText;

    public bool gameOver;
   // public bool displayed;
    public int score;
    public int highScore;

    // private int highScoreMedium, highScoreHard;
    // //public Button restartButton;

    // public GameObject titleScreen;
    // public GameObject titleText;
    // public GameObject easyButton;
    // public GameObject mediumButton;
    // public GameObject hardButton;


    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public bool isPaused;

    public GameObject lives3;
    public GameObject lives2;
    public GameObject lives1;
    public GameObject lives0;

    // public GameObject runTimeUI;

    // private Rigidbody smallClocksRb;
    // public GameObject smallClocks;
    //public GameObject smallClocksDestination;
    //public bool isClockInstantiated;

    public bool gameHasStarted = false;
    //public enum LEVEL { EASY, MEDIUM, HARD }
    //public LEVEL level;

    // private DOTweenAnimation pointValueanimation;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLives(3);
        UpdateScore(0);
        StartCoroutine(SpawnMonsters());
        StartCoroutine(SpawnBomb());
        gameOver = false;
        gameHasStarted = true;
        Time.timeScale = 1f;
        highScore = PlayerPrefs.GetInt("HighScore");
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnMonsters()
    {
        while(!gameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(1, targets.Count);
            Instantiate(targets[index]);
        }
    }
    IEnumerator SpawnBomb()
    {
        while(!gameOver)
        {
            yield return new WaitForSeconds(spawnRate*2);
            int indexMon = 0;
            Instantiate(targets[indexMon]);
        }
    }

    public void StopAllMonsters()
    {   
        for (int i=0 ; i<targets.Count ; i++)
        {
            //Destroy(targets[i].GetComponent<Rigidbody>());
            targets[i].GetComponent<Rigidbody>();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score +=scoreToAdd;
        scoreText.text = "" + score;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }

        highScoreText.text = "BEST : " + PlayerPrefs.GetInt("HighScore").ToString();
        scoreTextOnGameOver.text = "SCORE : " + scoreText.text;
        bestScoreOnGameOver.text = highScoreText.text;
  
    }

    public void UpdateLives(int livesToAdd)
    {
        lives += livesToAdd;
        //livesText.text = "Lives : " + lives;
        if (lives == 2 )
        {
            lives3.SetActive(true);
            lives2.SetActive(false);
            lives1.SetActive(false);
            lives0.SetActive(false);

        }
        if (lives == 2)
        {
            lives2.SetActive(true);
            lives3.SetActive(false);
            lives1.SetActive(false);
            lives0.SetActive(false);
        }
        if (lives == 1)
        {
            lives1.SetActive(true);
            lives3.SetActive(false);
            lives2.SetActive(false);
            lives0.SetActive(false);
        }
        if (lives <= 0)
        {
            lives0.SetActive(true);
            lives3.SetActive(false);
            lives2.SetActive(false);
            lives1.SetActive(false);
            GameOver();
        }
  
    }

    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        Time.timeScale = 0;

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

    public void CheckForPause()
    { 
        if(!gameOver)
        {
            if (!isPaused)
            {
                isPaused = true;
                pauseScreen.SetActive(true);
                Time.timeScale = 0;

            }

            else
            {
                isPaused = false;
                pauseScreen.SetActive(false);
                Time.timeScale = 1;
            }
        }

    }

    public void PanelActivate(GameObject thisGameObject)
    {
        thisGameObject.SetActive(true);
    }

    public void PanelDeactivate(GameObject thisGameObject)
    {
        thisGameObject.SetActive(false);
    }

    public void LoadOtherScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
