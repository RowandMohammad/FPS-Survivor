using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    #region Public Fields

    public float ammoPrice = 200;
    public bool AmmoStation;
    public float healthPrice = 5000;
    public bool HealthStation;
    public Text priceNumber;
    public Text priceText;

    #endregion Public Fields



    #region Private Fields

    private bool playerIsInReach = false;
    private PlayerManager playerManager;

    #endregion Private Fields



    #region Public Methods

    //BuyShop is called when interacting wiht shops
    public void BuyShop()
    {
        BuyHealth();
        BuyAmmo();
    }

    #endregion Public Methods



    #region Private Methods

    //Enables player to replenish ammo at the shop
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

    //Enables player to replenish health at the shop
    private void BuyHealth()
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

    //Detects whether player comes into reach distance to interact with shop
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

    //Detects whether player leaves the reach distance to interact with shop
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            priceNumber.gameObject.SetActive(false);
            priceText.gameObject.SetActive(false);
            playerIsInReach = false;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (HealthStation)
        {
            priceNumber.text = healthPrice.ToString();
        }
        if (AmmoStation)
        {
            priceNumber.text = ammoPrice.ToString();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerIsInReach)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyShop();
            }
        }
    }

    #endregion Private Methods
}