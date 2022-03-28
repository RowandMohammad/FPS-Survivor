using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class BasicZombieController : MonoBehaviour, IDamageable

{
    public float health = 100f;
    private bool isDead;
    public GameObject player;
    public Animator enemyAnimator;
    [SerializeField] float distanceToStop;

    private void awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distanceToPlayer < distanceToStop)
        {
            StopBefore();
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
        health -= damage;

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

    private void Die()
    {



    }

}
