using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Enemy))]
public class AgentDestructible : MonoBehaviour
{
    #region Private Fields

    [SerializeField]
    private NavMeshAgent Agent;

    private Coroutine AttackCoroutine;

    private Coroutine CheckCoroutine;

    [SerializeField]
    private float CheckDistance = 1f;

    [SerializeField]
    private int DestructibleAttackDamage = 10;

    [SerializeField]
    private float DestructibleAttackDelay = 1f;

    [SerializeField]
    private LayerMask DestructibleLayers;

    [SerializeField]
    private int DestructibleObjectCheckRate = 10;

    private Enemy Enemy;
    private NavMeshPath OriginalPath;

    #endregion Private Fields



    #region Private Methods
    //Destroys wall to reconfigure AI Path.
    private IEnumerator AttackDestructible(DestructibleWall Destructible)
    {
        WaitForSeconds Wait = new WaitForSeconds(DestructibleAttackDelay);
        while (Destructible != null)
        {
            Destructible.TakeDamage(DestructibleAttackDamage);

            yield return Wait;
        }
    }

    private void Awake()
    {
        Enemy = GetComponent<Enemy>();
        Enemy.OnStateChange += HandleStateChange;
    }

    //Coroutine to check if the AI can destroy objects
    private IEnumerator CheckForDestructibleObjects()
    {
        yield return null;
        WaitForSeconds Wait = new WaitForSeconds(1f / DestructibleObjectCheckRate);
        Vector3[] corners = new Vector3[2];

        bool foundDestructible = false;
        while (!foundDestructible)
        {
            int length = Agent.path.GetCornersNonAlloc(corners);
            if (length > 1 &&
                Physics.Raycast(
                    corners[0],
                    (corners[1] - corners[0]).normalized,
                    out RaycastHit hit,
                    CheckDistance,
                    DestructibleLayers) &&
                    hit.collider.TryGetComponent(out DestructibleWall destructible)
                )
            {
                destructible.OnDestroy += HandleDestroy;
                OriginalPath = Agent.path;
                Agent.enabled = false;
                Enemy.ChangeState(StateEnemy.Destroy);

                StopCoroutine(CheckCoroutine);
                AttackCoroutine = StartCoroutine(AttackDestructible(destructible));

                foundDestructible = true;
                break;
            }

            yield return Wait;
        }
    }

    private void HandleDestroy()
    {
        if (AttackCoroutine != null)
        {
            StopCoroutine(AttackCoroutine);
        }

        if (Enemy.State == StateEnemy.Destroy)
        {
            Agent.enabled = true;
            Agent.SetPath(OriginalPath);
            Enemy.ChangeState(StateEnemy.Chase);
        }
    }

    private void HandleStateChange(StateEnemy OldState, StateEnemy NewState)
    {
        if (NewState == StateEnemy.Chase)
        {
            if (CheckCoroutine != null)
            {
                StopCoroutine(CheckCoroutine);
            }
            if (AttackCoroutine != null)
            {
                StopCoroutine(AttackCoroutine);
            }
            CheckCoroutine = StartCoroutine(CheckForDestructibleObjects());
        }
    }

    #endregion Private Methods
}