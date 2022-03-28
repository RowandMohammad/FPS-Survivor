using Photon.Pun;
using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable 
{
    #region Private Fields
    [Header("Health Stats")]
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float timeBeforeRegen = 3;
    [SerializeField] private float healthValueIncrement = 3;
    [SerializeField] private float healthTimeIncrement = 0.1f;
    [SerializeField] private float currentHealth;
    private Coroutine regeneratingHealth;


    



     public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            KillPlayer();
        else if (regeneratingHealth != null)
            StopCoroutine(regeneratingHealth);

        regeneratingHealth = StartCoroutine(HealthRegenerate());
    }

    private void Update()
    {
        Debug.Log(currentHealth);
    }

    private IEnumerator HealthRegenerate()
    {
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
    }

    #endregion Private Fields



    #region Private Methods

    private void Awake()
    {
        currentHealth = maxHealth;
        
    }

    private void CreateController()
    {
        
    }

    private void Start()
    {
    
    }


    #endregion Private Methods
}