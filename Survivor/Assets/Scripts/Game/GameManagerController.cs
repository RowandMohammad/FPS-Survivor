using TMPro;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    #region Public Fields

    public float currScore = 0;
    public GameObject gameOverScreen;
    public GameObject gamePauseScreen;
    public TextMeshProUGUI scoreText;

    #endregion Public Fields



    #region Private Fields

    private Spawner spawner;

    #endregion Private Fields



    #region Public Methods

    // Adds score to current score of game.
    public void AddScore(float amount)
    {
        currScore += amount;
        UpdateScoreUI();
    }

    //Ends the game.
    public void GameOver()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gameOverScreen.GetComponent<GameOverScreen>().Setup(spawner.round);
    }

    //Pauses the game.
    public void GamePause()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        gamePauseScreen.GetComponent<GamePauseScreen>().Setup();
    }

    //Updates the score of the current UI.
    public void UpdateScoreUI()
    {
        scoreText.text = currScore.ToString("0");
    }

    #endregion Public Methods



    #region Private Methods

    // Start is called before the first frame update and intialises objects.
    private void Start()
    {
        currScore = 0;
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        UpdateScoreUI();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateScoreUI();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GamePause();
        }
    }

    #endregion Private Methods
}