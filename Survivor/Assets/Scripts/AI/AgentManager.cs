using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class AgentManager : MonoBehaviour
{
    #region Private Fields

    private static AgentManager _Instance;
    private NavMeshSurface Surface;

    #endregion Private Fields



    #region Public Properties

    public static AgentManager Instance
    {
        get
        {
            return _Instance;
        }

        private set
        {
            _Instance = value;
        }
    }

    #endregion Public Properties



    #region Public Methods

    //Creates new navigation mesh surface for AI to route around.
    public void BakeNavMesh()
    {
        Surface.BuildNavMesh();
    }

    #endregion Public Methods



    #region Private Methods

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Multiple NavMeshManagers in the scene! Destroying {name}!");
            Destroy(gameObject);
            return;
        }

        Surface = GetComponent<NavMeshSurface>();
        Instance = this;
    }

    #endregion Private Methods
}