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
    WeaponScript gunScript;
    public int accuracyPercent;
    public int successfulHits;
    public int shootCounter;

    // Start is called before the first frame update
    void Start()
    {
        accuracyPercent =0;
        gunScript = GameObject.Find("Weapon").GetComponent<WeaponScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = "Score: " + successfulHits.ToString();
        accuracyDisplay.text = "Accuracy: " + accuracyPercent.ToString() + "%";
        if (shootCounter>0)
        {
            accuracyPercent = (int)Math.Round((double)(100 * successfulHits) / shootCounter);

        }


        if (Input.GetKeyDown(KeyCode.Z))
        {
            successfulHits = 0;
            shootCounter = 0;
            accuracyPercent = 0;
        }






    }
}
