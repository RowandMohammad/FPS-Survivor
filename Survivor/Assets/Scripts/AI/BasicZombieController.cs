using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BasicZombieController : MonoBehaviour, IEnemyDamageable

{

    public float addScoreAmount = 100;
    GameManagerController gameController;
    Spawner spawner;

    [Header("Health VFX/SFX")]
    [SerializeField] private AudioClip[] hurtSounds;
    private AudioSource healthAudioSource;
    public float health = 100f;
    public bool isDead;
    public GameObject player;
    public Animator enemyAnimator;
    [SerializeField] float distanceToStop;
    public GameObject rightFist;
    public GameObject leftFist;
    
    public AudioSource au;
    public bool isHeadshot;
    public float damageAmount = 20f;
    



    [SerializeField] private float attackDamage;
    private float lastAttackTime = 0;
    private float attackInterval = 2;
    public AudioClip au_death;

    private void awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthAudioSource = GetComponent<AudioSource>();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManagerController>();
        au = gameObject.GetComponent<AudioSource>();
        setRigidbodyState(true);
        spawner  = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
        GetComponent<Animator>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToPlayer < distanceToStop)
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
        if (spawner.round > 1)
        {
            damageAmount = 40f;
        }
    }

    private void AttackPlayer()
    {
        if(Time.time - lastAttackTime >= attackInterval )
        {
            lastAttackTime = Time.time;
            enemyAnimator.SetInteger("AttackIndex", Random.Range(0, 4));
            enemyAnimator.SetTrigger("Attack");


        }
    }


    private void activateFist()
    {
        rightFist.GetComponent<SphereCollider>().enabled = true;
    }

    private void deactivateFist()
    {
        rightFist.GetComponent<SphereCollider>().enabled = false;
    }

    private void ChaseTarget()
    {
        GetComponent<NavMeshAgent>().isStopped = false;
        GetComponent<NavMeshAgent>().destination = player.transform.position;
       
    }

    private void StopBefore()
    {
        GetComponent<NavMeshAgent>().isStopped = true;
    }

    public void TakeDamage(float damage)
    {
        GetComponent<NavMeshAgent>().enabled = false;
        health -= damage;
        enemyAnimator.SetInteger("HitIndex", Random.Range(0, 4));
        enemyAnimator.SetTrigger("isHit");


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

    private void animEnd()
    {
        GetComponent<NavMeshAgent>().enabled = true;
    }



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

    void setRigidbodyState(bool state)
    {

        Rigidbody[] rigidbodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }

        GetComponent<Rigidbody>().isKinematic = !state;

    }


    void setColliderState(bool state)
    {

        Collider[] colliders = GetComponentsInChildren<Collider>();

        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }

        GetComponent<Collider>().enabled = !state;

    }

}
