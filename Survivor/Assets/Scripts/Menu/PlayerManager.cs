using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour, IDamageable 
{
    #region Private Fields
    [Header("Health Stats")]
    [SerializeField] public float maxHealth = 100;
    [SerializeField] private float timeBeforeRegen = 3;
    [SerializeField] private float healthValueIncrement = 3;
    [SerializeField] private float healthTimeIncrement = 0.1f;
    [SerializeField] private float currentHealth;
    private Coroutine regeneratingHealth;
    public float chipSpeed = 2f;
    private float lerpTimer;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthNumber;

    public GameManagerController gameController;

    [Header("Health VFX/SFX")]
    [SerializeField] private Image hurtImage = null;
    [SerializeField] private AudioClip[] hurtSounds;
    Color color;
    private AudioSource healthAudioSource;

    private bool hasCollide = false;
    public GameObject popUp;
    public GameObject parentObject;
    public GameObject popUpH;

    private void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        healthAudioSource = GetComponent<AudioSource>();
        color = hurtImage.color;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerController>();

    }



    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            KillPlayer();
        }
        else if (regeneratingHealth != null)
        {
            StopCoroutine(regeneratingHealth);
        }
 
        StartCoroutine(HurtFlash());
        regeneratingHealth = StartCoroutine(HealthRegenerate());


    }

    public void UpdateHealthUI()
    {

        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = currentHealth / maxHealth;
        healthNumber.text = currentHealth.ToString("0");
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);

        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);

        }


    }



    IEnumerator HurtFlash()
    {
        hurtImage.enabled = true;
        color.a = 1f;
        hurtImage.color = color;
        healthAudioSource.clip = hurtSounds[Random.Range(0, hurtSounds.Length)];
        healthAudioSource.PlayOneShot(healthAudioSource.clip);
        yield return new WaitForSeconds(0.5f);
        hurtImage.enabled = false;


    }


    private void Update()
    {
        Debug.Log(currentHealth);
        UpdateHealthUI();
    }

    public IEnumerator HealthRegenerate()
    {
        lerpTimer = 0;
        yield return new WaitForSeconds(timeBeforeRegen);
        WaitForSeconds waitingTime = new WaitForSeconds(healthTimeIncrement);

        while (currentHealth < maxHealth)
        {
            currentHealth += healthValueIncrement;
            
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            yield return waitingTime;
        }
        regeneratingHealth = null;


    }

    private void KillPlayer()
    {
        currentHealth = 0;
        if (regeneratingHealth != null)
            StopCoroutine(regeneratingHealth);
        gameController.GameOver();
    }


    #endregion Private Fields



    #region Private Methods

    private void Awake()
    {
        currentHealth = maxHealth;
        
    }

    IEnumerator collisionDetection()
    {
        yield return new WaitForSeconds(1f);
        hasCollide = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "ZombieFist" && !other.gameObject.GetComponentInParent<BasicZombieController>().isDead)
        {
            if (hasCollide == false)
            {
                hasCollide = true;
                TakeDamage(other.gameObject.GetComponentInParent<BasicZombieController>().damageAmount);
                StartCoroutine(collisionDetection());
            }
        }
    }
   

    public void PopUpActive()
    {
        Instantiate(popUp, transform.position, Quaternion.identity, parentObject.transform);
        
        
    }
    public void PopUpHeadShotActive()
    {
        Instantiate(popUpH, transform.position, Quaternion.identity, parentObject.transform);
        
    }






    #endregion Private Methods
}