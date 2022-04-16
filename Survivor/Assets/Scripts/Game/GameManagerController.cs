using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
  

    float currScore = 0;
  
    public TextMeshProUGUI scoreText;


    // Start is called before the first frame update
    void Start()
    {
        currScore = 0;
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        UpdateScoreUI();
        
        

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
    // Update is called once per frame
    void Update()
    {
        
    }
}
