using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{

    public GameObject gameOverScreen;
    public GameObject gamePauseScreen;
    public float currScore = 0;
    Spawner spawner;
  

    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        currScore = 0;
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        UpdateScoreUI();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();



    }

    public void AddScore(float amount)
    {
        currScore += amount;
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        scoreText.text = currScore.ToString("0");
    }

    public void GamePause()
    {
        
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gamePauseScreen.GetComponent<GamePauseScreen>().Setup();

    }


    public void GameOver()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gameOverScreen.GetComponent<GameOverScreen>().Setup(spawner.round);
    }
    // Update is called once per frame
    void Update()
    {
        UpdateScoreUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }
        
    }
}
