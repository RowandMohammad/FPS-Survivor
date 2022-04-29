using System.Collections;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    #region Public Fields

    public AudioClip begginingSong;

    public AudioSource playerAudio;

    public int round = 1;

    public AudioClip roundSong;

    public GameObject roundText;

    public TextMeshProUGUI roundTextTMP;

    public GameObject[] spawnPoints;

    public SpawnState state = SpawnState.COUNTING;

    public float timeBetweenWaves = 10f;

    public float waveCountdown;

    public Wave[] waves;

    #endregion Public Fields



    #region Private Fields

    private int nextWave = 0;

    private float searchCountdown = 1f;

    #endregion Private Fields



    #region Public Enums
    //The different states of rounds.
    public enum SpawnState
    { SPAWNING, WAITING, COUNTING }

    #endregion Public Enums



    #region Private Methods
    //Checks whether any more enemy Ai is alive.
    private bool EnemyIsAlive()
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

    //Changes the round number upon successful wave when invoked.
    private void invokeRoundNumber()
    {
        roundTextTMP.text = round.ToString("0");
    }
    

    //Deals with all the effects and round updates after a successful wave.
    private void roundCompleted()
    {
        round++;
        roundModifier();
        updateRoundUI();
        playerAudio.PlayOneShot(roundSong);
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
    }

    //Modifies the count of AI spawning each round.
    private void roundModifier()
    {
        if (round > 1)
        {
            waves[nextWave].count = waves[nextWave].count + 3;
        }
    }

    //Spawns enemy AI.
    private void SpawnEnemy(GameObject _enemy)
    {
        GameObject spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject zombieSpawned = Instantiate(_enemy, spawnPoint.transform.position, Quaternion.identity);
    }

    // handles the management of enemy AI spawining in.
    private IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1 / _wave.spawnRate);
        }

        state = SpawnState.WAITING;

        yield break;
    }

    private void Start()
    {
        roundText = GameObject.FindGameObjectWithTag("RoundText");
        roundTextTMP = roundText.GetComponent<TextMeshProUGUI>();
        waveCountdown = timeBetweenWaves;
        playerAudio = GameObject.Find("Player").GetComponent<AudioSource>();
        playerAudio.PlayOneShot(begginingSong);
    }

    private void Update()
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

        if (waveCountdown <= 0)
        {
            if (state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    private void updateRoundUI()
    {
        roundText.GetComponent<Animator>().SetTrigger("newRound");
        Invoke("invokeRoundNumber", 0.25f);
    }

    #endregion Private Methods



    #region Public Classes

    [System.Serializable]
    public class Wave
    {
        #region Public Fields

        public int count;
        public GameObject enemy;
        public string name;
        public float spawnRate;

        #endregion Public Fields
    }

    #endregion Public Classes

    // Start is called before the first frame update

    // Update is called once per frame
}