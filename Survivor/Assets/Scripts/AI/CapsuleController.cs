using UnityEngine;

public class CapsuleController : MonoBehaviour, IEnemyDamageable

{
    #region Public Fields

    public float health = 10f;

    #endregion Public Fields



    #region Private Fields

    private System.Random _random = new System.Random();
    private bool isDead;

    #endregion Private Fields



    #region Public Delegates

    public delegate void CapsuleDestroyed();

    #endregion Public Delegates



    #region Public Events

    public static event CapsuleDestroyed OnCapsuleDestroyed;

    #endregion Public Events



    #region Public Methods
    //Handles when training capsule takes damage
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

    #endregion Public Methods



    #region Private Methods

    private void awake()
    {
    }

    //Handles capsule objects death.
    private void Die()
    {
        Destroy(gameObject);
        GetComponent<Collider>().enabled = false;
        if (OnCapsuleDestroyed != null)
        {
            OnCapsuleDestroyed();
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #endregion Private Methods
}