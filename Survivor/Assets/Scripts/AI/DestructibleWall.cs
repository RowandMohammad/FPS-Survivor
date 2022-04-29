using UnityEngine;

public class DestructibleWall : MonoBehaviour, IDamageable
{
    #region Public Fields

    public float Health;

    public DestryEvent OnDestroy;

    public TakeDamageEvent OnTakeDamage;

    #endregion Public Fields



    #region Private Fields

    [SerializeField]
    private ParticleSystem DestroySystem;

    #endregion Private Fields



    #region Public Delegates

    public delegate void DestryEvent();

    public delegate void TakeDamageEvent(float Damage, float Health);

    #endregion Public Delegates



    #region Public Methods
    //Handles destrcutible wall taking damage.
    public void TakeDamage(float Damage)
    {
        Health -= Damage;
        if (Health <= 0)
        {
            Health = 0;
            OnTakeDamage?.Invoke(Damage, Health);
            if (DestroySystem != null)
            {
                DestroySystem.gameObject.SetActive(true);
                DestroySystem.transform.SetParent(null, true);
                DestroySystem.Play();
            }
            OnDestroy?.Invoke();

            gameObject.SetActive(false);

            AgentManager.Instance.BakeNavMesh();

            Destroy(gameObject);
        }
        else
        {
            OnTakeDamage?.Invoke(Damage, Health);
        }
    }

    #endregion Public Methods
}