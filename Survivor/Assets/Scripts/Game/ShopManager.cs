using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public float healthPrice = 5000;
    public float ammoPrice = 200;

    public Text priceText;

    public Text priceNumber;

    PlayerManager playerManager;

    bool playerIsInReach = false;

    public bool HealthStation;
    public bool AmmoStation;

    // Start is called before the first frame update
    void Start()
    {
        if (HealthStation)
        {
            priceNumber.text =  healthPrice.ToString();
        }
        if (AmmoStation)
        {
            priceNumber.text = ammoPrice.ToString();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsInReach)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyShop();
            }
        }
    }

    public void BuyShop()
    {
        BuyHealth();
        BuyAmmo();

    }

    private void BuyAmmo()
    {
        if (AmmoStation)
        {
            if (playerManager.gameController.currScore >= ammoPrice)

            {
                playerManager.gameController.currScore -= ammoPrice;
                WeaponScript weaponScript = GameObject.FindGameObjectWithTag("Weapon").GetComponent<WeaponScript>();
                weaponScript.totalAmmo = weaponScript.maxAmmo;


            }

            else

            {

                Debug.Log("Poor");

            }

        }
    }

    void BuyHealth()
    {
        if (HealthStation)
        {
            if (playerManager.gameController.currScore >= healthPrice)

            {


                playerManager.gameController.currScore -= healthPrice;
                playerManager.maxHealth = 250f;


            }

            else

            {

                Debug.Log("Poor");

            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            priceNumber.gameObject.SetActive(true);
            priceText.gameObject.SetActive(true);
            playerIsInReach = true;
            playerManager = other.GetComponent<PlayerManager>();
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            priceNumber.gameObject.SetActive(false);
            priceText.gameObject.SetActive(false);
            playerIsInReach = false;
            
        }


    }
}




