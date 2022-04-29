using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    #region Public Fields

    public TextMeshProUGUI roundsText;

    #endregion Public Fields



    #region Public Methods
    // exits player out of the game
    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    // restarts the game scene
    public void RestartButton()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void Setup(float round)
    {
        gameObject.SetActive(true);
        roundsText.text = "You survived " + round.ToString() + " rounds";
    }

    #endregion Public Methods



    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #endregion Private Methods
}