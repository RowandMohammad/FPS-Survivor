using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleManager : MonoBehaviour
{

    public GameObject m_EnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        SpawnNewEnemy();
    }

    void OnEnable()
    {
        CapsuleController.OnCapsuleDestroyed += SpawnNewEnemy;
    }


    void SpawnNewEnemy()
    {
        Vector3 spawnPoint = new Vector3(Random.Range(-50f, -29f), 1f, Random.Range(-4f, 10f));


        Instantiate(m_EnemyPrefab, spawnPoint, Quaternion.identity);


    }

}