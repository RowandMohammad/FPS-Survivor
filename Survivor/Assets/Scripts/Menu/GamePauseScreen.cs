using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseScreen : MonoBehaviour
{
    #region Public Methods

    //resumes the game scene.
    public void ContinueButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // exits game scene back to main menu
    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Setup()
    {
        gameObject.SetActive(true);
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