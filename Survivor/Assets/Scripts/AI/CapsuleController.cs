using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CapsuleController : MonoBehaviour, IDamageable

{
    public float health = 10f;

    System.Random _random = new System.Random();
    private bool isDead;

    public delegate void CapsuleDestroyed();
    public static event CapsuleDestroyed OnCapsuleDestroyed;

    private void awake()
    {
     
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        
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

        Destroy(gameObject);
        GetComponent<Collider>().enabled = false;
        if (OnCapsuleDestroyed != null)
        {
            OnCapsuleDestroyed();
        }


    }

    }