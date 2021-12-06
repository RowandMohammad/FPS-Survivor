using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{

    [SerializeField] private Text scoreDisplay;
    [SerializeField] private Text accuracyDisplay;
    ItemController gunScript;
    public int accuracyPercent;
    public int successfulHits;

    // Start is called before the first frame update
    void Start()
    {
        accuracyPercent =0;
        gunScript = GameObject.Find("ItemHolder").GetComponent<ItemController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = "Score: " + successfulHits.ToString();
        accuracyDisplay.text = "Accuracy: " + accuracyPercent.ToString() + "%";
        if (gunScript.shootCounter>0)
        {
            accuracyPercent = (int)Math.Round((double)(100 * successfulHits) / gunScript.shootCounter);

        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            successfulHits = 0;
            gunScript.shootCounter = 0;
            accuracyPercent = 0;
        }






    }
}
