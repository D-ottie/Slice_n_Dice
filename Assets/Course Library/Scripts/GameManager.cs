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
    public List<GameObject> clockPrefab;

    private float spawnRate = 1.0f;
    public int countdown = 60;
    public int lives;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    //public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI counterText;
    //public TextMeshProUGUI livesText;
    public TextMeshProUGUI pointsText;

    public bool gameOver;
   // public bool displayed;
    public int score;
    public int highScore;
    private int highScoreMedium, highScoreHard;
    //public Button restartButton;

    public GameObject titleScreen;
    public GameObject titleText;
    public GameObject easyButton;
    public GameObject mediumButton;
    public GameObject hardButton;


    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public bool isPaused;

    public GameObject lives3;
    public GameObject lives2;
    public GameObject lives1;
    public GameObject lives0;

    public GameObject runTimeUI;

    private Rigidbody smallClocksRb;
    public GameObject smallClocks;
    public GameObject smallClocksDestination;
    public bool isClockInstantiated;

    public bool gameHasStarted = false;
    public enum LEVEL { EASY, MEDIUM, HARD }
    public LEVEL level;

    // private DOTweenAnimation pointValueanimation;

    // Start is called before the first frame update
    void Start()
    {
        //pointValueanimation = pointsText.GetComponent<DOTweenAnimation>();
        runTimeUI.SetActive(false);
        smallClocksRb = smallClocks.GetComponent<Rigidbody>();

        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreMedium = PlayerPrefs.GetInt("HighScoreMedium");
        highScoreHard = PlayerPrefs.GetInt("HighScoreHard");

        titleText.transform.DOShakeScale(5f, new Vector3(1, 1, 1), 10, 90);
    }



    // Update is called once per frame
    void Update()
    {
        if(isClockInstantiated)
        {
            var clocks = GameObject.FindGameObjectsWithTag("SmallClock");
            Debug.Log("small clocks instantiated");
            //smallClocks.transform.Translate(smallClocksDestination.transform.position  * 50);
            clocks[0].transform.DOMove(smallClocksDestination.transform.position, 3f);
            if(clocks[0].transform.position.x > 7.0f)
            {
                clocks[0].SetActive(false);
                isClockInstantiated = false;
            }
            
            
        }

          //smallClocksRb.AddForce(smallClocksDestination.transform.position * 50, ForceMode.VelocityChange);
   
    }

    public void SetLevel(string level)
    {
        if (level.Equals("Easy")) this.level = LEVEL.EASY;
        if (level.Equals("Medium")) this.level = LEVEL.MEDIUM;
        if (level.Equals("Hard")) this.level = LEVEL.HARD;
    }

    public void StartGame(int difficulty)
    {
        spawnRate = spawnRate / difficulty;
        StartCoroutine(SpawnClock());
        StartCoroutine(SpawnMonsters());
        StartCoroutine(CountDown());
        UpdateLives(3);
        UpdateScore(0, level);
        gameOver = false;
        titleScreen.SetActive(false);
        gameHasStarted = true;
        Time.timeScale = 1f;
        runTimeUI.SetActive(true);
    }

    IEnumerator SpawnMonsters()
    {
        while(!gameOver)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);

        }
    }

    IEnumerator SpawnClock()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(15);
            int clockIndex = Random.Range(0, clockPrefab.Count);
            Instantiate(clockPrefab[clockIndex]);
        }
    }


    IEnumerator CountDown()
    {

        counterText.text = "" + countdown;
        while(true)
        {
            yield return new WaitForSeconds(1);
            counterText.text = "" + countdown;
            countdown -=1;

            if(countdown == 0)
            {
                GameOver();
            }
        }
    }

    
    public void UpdateScore(int scoreToAdd, LEVEL level)
    {
        score +=scoreToAdd;
        scoreText.text = "" + score;
        /*Debug.Log(" + " + scoreToAdd + " = " + score);*/
        switch (level)
        {
            case LEVEL.EASY:
                if (score > highScore)
                {
                    highScore = score;
                    PlayerPrefs.SetInt("HighScore", score);
                }
                highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
                break;
            case LEVEL.MEDIUM:
                if (score > highScoreMedium)
                {
                    highScoreMedium = score;
                    PlayerPrefs.SetInt("HighScoreMedium", highScoreMedium);
                }
                highScoreText.text = PlayerPrefs.GetInt("HighScoreMedium").ToString();
                break;
            case LEVEL.HARD:
                if (score > highScoreHard )
                {
                    highScoreHard = score;
                    PlayerPrefs.SetInt("HighScoreHard", highScoreHard);
                }
                highScoreText.text = PlayerPrefs.GetInt("HighScoreHard").ToString();
                break;
        }
        
        
    }

/*    public void DisplayScore()
    {

        pointsText.gameObject.SetActive(true);
        pointsText.text =""+ FindObjectOfType<Targets>().pointValue;
        pointsText.transform.DOShakeScale(1f, new Vector3(1,1,1), 2 , 5 , true);
        //pointValueanimation.enabled = true;

    }*/

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


}
