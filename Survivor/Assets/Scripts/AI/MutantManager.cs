using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class MutantManager : MonoBehaviour
{
    #region Public Fields

    public List<MutantController> baseUnits = new List<MutantController>();
    public float playerHealth = 100f;
    public float RadiusAroundTarget = 5f;
    public Transform Target;
    public List<MutantController> Units = new List<MutantController>();

    #endregion Public Fields



    #region Private Fields

    private static MutantManager _instance;

    #endregion Private Fields



    #region Public Properties

    public static MutantManager Instance
    {
        get
        {
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    #endregion Public Properties



    #region Private Methods

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }

    //Makes AI Mutant move into its given position in circling of player.
    private void MakeAgentsCircleTarget()
    {
        for (int i = 0; i < Units.Count; i++)
        {
            Units[i].MoveTo(new Vector3(
                Target.position.x + RadiusAroundTarget * Mathf.Cos(2 * Mathf.PI * i / Units.Count),
                Target.position.y,
                Target.position.z + RadiusAroundTarget * Mathf.Sin(2 * Mathf.PI * i / Units.Count)
                ));
        }
    }

    private void Update()
    {
        if (playerHealth <= 40 && baseUnits.Count < 1)
        {
            baseUnits.Add(Units[Units.Count - 1]);
            Units.Remove(Units[Units.Count - 1]);
        }
        MakeAgentsCircleTarget();
    }

    #endregion Private Methods
}