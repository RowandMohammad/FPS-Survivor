using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class EnemyLineOfSightChecker : MonoBehaviour
{
    #region Public Fields

    public SphereCollider Collider;
    public float FieldOfView;
    public LayerMask LineOfSightLayers;

    public GainSightEvent OnGainSight;

    public LoseSightEvent OnLoseSight;

    #endregion Public Fields



    #region Private Fields

    private Coroutine CheckForLineOfSightCoroutine;

    #endregion Private Fields



    #region Public Delegates

    public delegate void GainSightEvent(Transform Target);

    public delegate void LoseSightEvent(Transform Target);

    #endregion Public Delegates



    #region Private Methods

    private void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }
    //Coroutine to constantly check whether state of player is not in the line of sight.
    private IEnumerator CheckForLineOfSight(Transform Target)
    {
        WaitForSeconds Wait = new WaitForSeconds(0.5f);

        while (!CheckLineOfSight(Target))
        {
            yield return Wait;
        }
    }

    //Checks whether play is within line of sight with given field of view.
    private bool CheckLineOfSight(Transform Target)
    {
        Vector3 direction = (Target.transform.position - transform.position).normalized;
        float dotProduct = Vector3.Dot(transform.forward, direction);
        if (dotProduct >= Mathf.Cos(FieldOfView))
        {
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, Collider.radius, LineOfSightLayers))
            {
                OnGainSight?.Invoke(Target);
                return true;
            }
        }

        return false;
    }

   
    //Activated when a user enters the AI agents sphere collider to detected whether it is too close and the AI should hide.
    private void OnTriggerEnter(Collider other)
    {
        if (!CheckLineOfSight(other.transform))
        {
            CheckForLineOfSightCoroutine = StartCoroutine(CheckForLineOfSight(other.transform));
        }
    }

    //Lets AI know the player user has exited its sphere collider to detect if it should hide from player.
    private void OnTriggerExit(Collider other)
    {
        OnLoseSight?.Invoke(other.transform);
        if (CheckForLineOfSightCoroutine != null)
        {
            StopCoroutine(CheckForLineOfSightCoroutine);
        }
    }

    #endregion Private Methods
}