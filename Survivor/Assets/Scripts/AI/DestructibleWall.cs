using UnityEngine;

public class DestructibleWall : MonoBehaviour, IDamageable
{
    [SerializeField]
    private ParticleSystem DestroySystem;

    public float Health;

    public delegate void DestryEvent();
    public DestryEvent OnDestroy;

    public delegate void TakeDamageEvent(float Damage, float Health);
    public TakeDamageEvent OnTakeDamage;

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
}