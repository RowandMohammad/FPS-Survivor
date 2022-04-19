using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public enum SpawnState { SPAWNING, WAITING, COUNTING}
    [System.Serializable]
    public class Wave
    {
        public string name;
        public GameObject enemy;
        public int count;
        public float spawnRate;

    }

    public Wave[] waves;
    private int nextWave = 0;
    public int round = 1;


    public float timeBetweenWaves = 10f;
    public float waveCountdown;
    public SpawnState state = SpawnState.COUNTING;

    private float searchCountdown = 1f;
    public GameObject[] spawnPoints;

    public GameObject roundText;
    public TextMeshProUGUI roundTextTMP;
    public AudioSource playerAudio;
    public AudioClip begginingSong;
    public AudioClip roundSong;
    void Start()
    {
        roundText = GameObject.FindGameObjectWithTag("RoundText");
        roundTextTMP = roundText.GetComponent<TextMeshProUGUI>();
        waveCountdown = timeBetweenWaves;
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();
        playerAudio.PlayOneShot(begginingSong);

    }

    void updateRoundUI()
    {
        roundText.GetComponent<Animator>().SetTrigger("newRound");
        Invoke("invokeRoundNumber", 0.25f);

    }

    void invokeRoundNumber()
    {
        roundTextTMP.text = round.ToString("0");

    }

    void roundCompleted()
    {
        round++;
        roundModifier(); 
        updateRoundUI();
        playerAudio.PlayOneShot(roundSong);
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;


        
    }
    void roundModifier()
    {
        if (round > 1)
        {
            waves[nextWave].count = waves[nextWave].count + 3;
        }
    }
    void Update()
    { 
        if (state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                roundCompleted();
            }
            else
            {
                return;
            }
        }

        if (waveCountdown <=0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
 


        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
        

    }



    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            if (GameObject.FindGameObjectWithTag("Zombie") == null)
            {
                return false;
            }
        }


        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for(int i=0; i<_wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1 / _wave.spawnRate);
        }

        state = SpawnState.WAITING;

        yield break;

    }

    void SpawnEnemy( GameObject _enemy)
    {
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject zombieSpawned = Instantiate(_enemy, spawnPoint.transform.position, Quaternion.identity);
        
    }



 





    // Start is called before the first frame update


    // Update is called once per frame





}
