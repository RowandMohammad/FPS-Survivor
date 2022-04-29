using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    #region Public Fields

    public int accuracyPercent;
    public int shootCounter;
    public int successfulHits;

    #endregion Public Fields



    #region Private Fields

    [SerializeField] private Text accuracyDisplay;
    private WeaponScript gunScript;
    [SerializeField] private Text scoreDisplay;

    #endregion Private Fields



    #region Private Methods

    // Start is called before the first frame update
    private void Start()
    {
        accuracyPercent = 0;
        gunScript = GameObject.Find("Weapon").GetComponent<WeaponScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        scoreDisplay.text = "Score: " + successfulHits.ToString();
        accuracyDisplay.text = "Accuracy: " + accuracyPercent.ToString() + "%";
        if (shootCounter > 0)
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

    #endregion Private Methods
}