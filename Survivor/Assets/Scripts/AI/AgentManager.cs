using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshSurface))]
public class AgentManager : MonoBehaviour
{
    private NavMeshSurface Surface;

    private static AgentManager _Instance;
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

    public void BakeNavMesh()
    {
        Surface.BuildNavMesh();
    }
}