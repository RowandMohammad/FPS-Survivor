using Photon.Pun;
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
    private float currentHealth;
    private Coroutine regeneratingHealth;



     public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
            KillPlayer();
        else if (regeneratingHealth != null)
            StopCoroutine(regeneratingHealth);
    }

    private void KillPlayer()
    {

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