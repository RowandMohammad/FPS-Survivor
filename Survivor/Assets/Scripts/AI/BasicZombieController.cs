using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BasicZombieController : MonoBehaviour, IEnemyDamageable

{
    #region Public Fields

    public float addScoreAmount = 100;
    public AudioSource au;
    public AudioClip au_death;
    public float damageAmount = 20f;
    public Animator enemyAnimator;
    public float health = 100f;
    public bool isDead;
    public bool isHeadshot;
    public GameObject leftFist;
    public GameObject player;
    public GameObject rightFist;
    public Slider slider;

    #endregion Public Fields



    #region Private Fields

    [SerializeField] private float attackDamage;
    private float attackInterval = 2;
    [SerializeField] private float distanceToStop;
    private GameManagerController gameController;
    private AudioSource healthAudioSource;

    [Header("Health VFX/SFX")]
    [SerializeField] private AudioClip[] hurtSounds;

    private float lastAttackTime = 0;
    private float maxHealth = 100f;
    private Spawner spawner;

    #endregion Private Fields



    #region Public Methods
    //When Zombie AI takes damage.
    public void TakeDamage(float damage)
    {
        GetComponent<NavMeshAgent>().enabled = false;
        health -= damage;
        enemyAnimator.SetInteger("HitIndex", Random.Range(0, 4));
        enemyAnimator.SetTrigger("isHit");
        slider.value = health;

        if (health <= 0 && isDead != true)
        {
            isDead = true;
            Die();
        }
        if (health > 0 && isDead != true)
        {
            healthAudioSource.clip = hurtSounds[Random.Range(0, hurtSounds.Length)];
            healthAudioSource.PlayOneShot(healthAudioSource.clip);

            print(health);
        }
    }

    #endregion Public Methods



    #region Private Methods

    //Activates collider in zombies fist to allow collision between player and AI to handle damage.
    private void activateFist()
    {
        rightFist.GetComponent<SphereCollider>().enabled = true;
    }
    //Allows user to restart itr normal behaviour after taking damage animation.
    private void animEnd()
    {
        GetComponent<NavMeshAgent>().enabled = true;
    }

    //Handles AI attacking a player user.
    private void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackInterval)
        {
            lastAttackTime = Time.time;
            enemyAnimator.SetInteger("AttackIndex", Random.Range(0, 4));
            enemyAnimator.SetTrigger("Attack");
        }
    }

    private void awake()
    {
    }

    //Routes AI towards the player
    private void ChaseTarget()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<NavMeshAgent>().destination = player.transform.position;
    }

    //Deactivates collider in zombies fist to now allow collision between player and AI to handle damage.
    private void deactivateFist()
    {
        rightFist.GetComponent<SphereCollider>().enabled = false;
    }

    //Handles AI death
    private void Die()
    {
        au.PlayOneShot(au_death);
        GetComponent<Animator>().enabled = false;

        setRigidbodyState(false);
        if (isHeadshot)
        {
            player.GetComponent<PlayerManager>().PopUpHeadShotActive();
            gameController.AddScore(150);
        }
        else
        {
            player.GetComponent<PlayerManager>().PopUpActive();
            gameController.AddScore(addScoreAmount);
        }

        if (gameObject != null)
        {
            Destroy(gameObject, 3f);
        }
    }

    //Sets colliders in Object to change state so AI becomes ragdoll after death.
    private void setColliderState(bool state)
    {
        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;
    }

    //Sets rigibodies in Object to change state false so AI becomes ragdoll after death.
    private void setRigidbodyState(bool state)
    {
        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;
    }

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthAudioSource = GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerController>();
        au = gameObject.GetComponent<AudioSource>();
        setRigidbodyState(true);
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        GetComponent<Animator>().enabled = true;
        slider.maxValue = maxHealth;
        slider.value = health;
    }

    //Stops Ai from getting too close to player
    private void StopBefore()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    // Update is called once per frame
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < distanceToStop)
        {
            StopBefore();
            AttackPlayer();
        }
        else
        {
            ChaseTarget();
        }

        if (GetComponent<NavMeshAgent>().velocity.magnitude > 1)
        {
            enemyAnimator.SetBool("isRunning", true);
        }
        else
        {
            enemyAnimator.SetBool("isRunning", false);
        }
        if (spawner.round > 4)
        {
            damageAmount = 40f;
        }
        if (spawner.round > 8)
        {
            damageAmount = 50f;
            GetComponent<NavMeshAgent>().speed = 6f;
        }

        slider.transform.LookAt(player.transform);
    }

    #endregion Private Methods
}