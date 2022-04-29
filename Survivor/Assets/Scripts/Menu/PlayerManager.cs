using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerManager : MonoBehaviour, IDamageable
{
    #region Public Fields

    public Image backHealthBar;

    public float chipSpeed = 2f;

    public Image frontHealthBar;

    public GameManagerController gameController;

    public TextMeshProUGUI healthNumber;

    [Header("Health Stats")]
    [SerializeField] public float maxHealth = 100;

    public GameObject parentObject;
    public GameObject popUp;
    public GameObject popUpH;

    #endregion Public Fields



    #region Private Fields

    private Color color;
    [SerializeField] private float currentHealth;
    private bool hasCollide = false;
    private AudioSource healthAudioSource;
    [SerializeField] private float healthTimeIncrement = 0.1f;
    [SerializeField] private float healthValueIncrement = 3;

    [Header("Health VFX/SFX")]
    [SerializeField] private Image hurtImage = null;

    [SerializeField] private AudioClip[] hurtSounds;
    private float lerpTimer;
    private Coroutine regeneratingHealth;
    [SerializeField] private float timeBeforeRegen = 3;

    #endregion Private Fields



    #region Public Methods
    //Regenerates player health after given time after taking damage.
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
    // creates visual effect of points whenplayer kills an AI 
    public void PopUpActive()
    {
        Instantiate(popUp, transform.position, Quaternion.identity, parentObject.transform);
    }

    // creates visual effect of points whenplayer kills an AI via headshot
    public void PopUpHeadShotActive()
    {
        Instantiate(popUpH, transform.position, Quaternion.identity, parentObject.transform);
    }

    // handles the user taking damage.
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


    // updates the health visuals of the player.
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

    #endregion Public Methods



    #region Private Methods

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    //waits for collider collision zombie fists to occur after one has occured
    private IEnumerator collisionDetection()
    {
        yield return new WaitForSeconds(1f);
        hasCollide = false;
    }

    //Plays taking damager visuals and audio
    private IEnumerator HurtFlash()
    {
        hurtImage.enabled = true;
        color.a = 1f;
        hurtImage.color = color;
        healthAudioSource.clip = hurtSounds[Random.Range(0, hurtSounds.Length)];
        healthAudioSource.PlayOneShot(healthAudioSource.clip);
        yield return new WaitForSeconds(0.5f);
        hurtImage.enabled = false;
    }


    // kills player user when called.
    private void KillPlayer()
    {
        currentHealth = 0;
        if (regeneratingHealth != null)
            StopCoroutine(regeneratingHealth);
        gameController.GameOver();
    }

    // detects whether enemy first collider makes contact with their collider to allow damage to be taken
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ZombieFist" && !other.gameObject.GetComponentInParent<BasicZombieController>().isDead)
        {
            if (hasCollide == false)
            {
                hasCollide = true;
                TakeDamage(other.gameObject.GetComponentInParent<BasicZombieController>().damageAmount);
                StartCoroutine(collisionDetection());
            }
        }
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        healthAudioSource = GetComponent<AudioSource>();
        color = hurtImage.color;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerController>();
    }

    private void Update()
    {
        Debug.Log(currentHealth);
        UpdateHealthUI();
    }

    #endregion Private Methods
}