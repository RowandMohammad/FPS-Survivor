using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BasicZombieController : MonoBehaviour, IEnemyDamageable

{

    [Header("Health VFX/SFX")]
    [SerializeField] private AudioClip[] hurtSounds;
    private AudioSource healthAudioSource;
    public float health = 100f;
    private bool isDead;
    public GameObject player;
    public Animator enemyAnimator;
    [SerializeField] float distanceToStop;
    public GameObject rightFist;
    public GameObject leftFist;
    public Spawner spawner;


    [SerializeField] private float attackDamage;
    private float lastAttackTime = 0;
    private float attackInterval = 2;

    private void awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        healthAudioSource = GetComponent<AudioSource>();
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
    }

    private void AttackPlayer()
    {
        if(Time.time - lastAttackTime >= attackInterval)
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
            
            print(health);

        }
        

    }

    private void animEnd()
    {
        GetComponent<NavMeshAgent>().enabled = true;
    }

    private void Die()
    {

        Destroy(gameObject);
        spawner.enemiesAlive--;


    }

}
