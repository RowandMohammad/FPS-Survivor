using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[DefaultExecutionOrder(1)]
public class MutantController : MonoBehaviour
{
    public NavMeshAgent Agent;
    public Transform Target;
    private Animator enemyAnimator;

    private void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
        
        enemyAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        MutantManager.Instance.Units.Add(this);
    }

    public void MoveTo(Vector3 Position)
    {
        Agent.SetDestination(Position);
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    private void Update()
    {
        RotateTowards(Target);


        if (GetComponent<NavMeshAgent>().velocity.magnitude > 1)
        {
            enemyAnimator.SetBool("isRunning", true);

        }
        else
        {
            enemyAnimator.SetBool("isRunning", false);
        }

    }
}