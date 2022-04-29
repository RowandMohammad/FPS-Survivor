using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Enemy))]
[DefaultExecutionOrder(1)]
public class MutantController : MonoBehaviour
{
    #region Private Fields

    private NavMeshAgent Agent;

    private Enemy Enemy;

    [SerializeField]
    private Transform Target;

    #endregion Private Fields



    #region Internal Methods

    internal void MoveTo(Vector3 vector3)
    {
        Agent.SetDestination(vector3);
    }

    #endregion Internal Methods



    #region Private Methods

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        MutantManager.Instance.Units.Add(this);
        Enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        Agent.enabled = true;
        Agent.SetDestination(Target.position);
        Enemy.ChangeState(StateEnemy.Chase);

        if (Agent.enabled && Agent.remainingDistance < Agent.radius)
        {
            Enemy.ChangeState(StateEnemy.Idle);
        }
    }

    #endregion Private Methods
}