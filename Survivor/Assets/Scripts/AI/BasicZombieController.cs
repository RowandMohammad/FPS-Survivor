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
        print(health);
        _animator.SetInteger("takeDamageIndex", Random.Range(0, 2));
        _animator.SetTrigger("takeDamage");






    }

    int animationRandomizer()
    {
        return _random.Next(1, 2);
        
    }

}
