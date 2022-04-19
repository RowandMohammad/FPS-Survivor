using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI roundsText;
    public void Setup(float round)
    {
        gameObject.SetActive(true);
        roundsText.text = "You survived " + round.ToString() + " rounds";
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
