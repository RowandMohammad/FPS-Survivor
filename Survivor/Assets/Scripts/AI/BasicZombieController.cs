using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicZombieController : MonoBehaviour, IDamageable

{
    public float health = 100f;
    [SerializeField] public Animator _animator;
    System.Random _random = new System.Random();
    private bool isDead;

    public delegate void ZombieKilled();
    public static event ZombieKilled OnZombieKilled;

    private void awake()
    {
        _animator = GetComponent<Animator>();
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
            _animator.SetInteger("takeDamageIndex", Random.Range(0, 3));
            _animator.SetTrigger("takeDamage");
        }
  


    }

    private void Die()
    {
        _animator.SetTrigger("onDied");
        Destroy(transform.parent.gameObject, 3f);
        GetComponent<Collider>().enabled = false;
        if (OnZombieKilled != null)
        {
            OnZombieKilled();
        }


    }

    int animationRandomizer()
    {
        return _random.Next(1, 2);
        
    }

}
