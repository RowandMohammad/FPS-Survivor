using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(Enemy))]
public class MutantController : MonoBehaviour
{

    [SerializeField]
    Transform Target;
    private Enemy Enemy;
    private NavMeshAgent Agent;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
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
}