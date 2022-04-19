using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePauseScreen : MonoBehaviour
{
   
    public void Setup()
    {
        gameObject.SetActive(true);
        
    }

    public void ContinueButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;

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