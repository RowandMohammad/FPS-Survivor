using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(0)]
public class MutantManager : MonoBehaviour
{
    private static MutantManager _instance;
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

    public Transform Target;
    public float RadiusAroundTarget = 5f;
    public List<MutantController> Units = new List<MutantController>();
    public List<MutantController> list = new List<MutantController>();
    public float playerHealth = 100f;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }

        Destroy(gameObject);
    }


    private void Update()
    {
        if (playerHealth <= 40 && list.Count < 1)
        {
            list.Add(Units[Units.Count - 1]);
            Units.Remove(Units[Units.Count - 1]);
        }
        MakeAgentsCircleTarget();

    }

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
}