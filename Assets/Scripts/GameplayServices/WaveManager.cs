using EnemyTankServices;
using GlobalServices;
using UIServices;
using UnityEngine;

public class WaveManager : MonoSingletonGeneric<WaveManager>
{
    [SerializeField] private int tankSpawnDelay = 3;

    private int currentWave = 0;
    private bool b_IsGameOver = false;
    private bool b_IsGamePaused = false;
    private bool b_IsWaveCompleted = true;

    private void OnDisable()
    {
        EventService.Instance.OnGameOver -= GameOver;
        EventService.Instance.OnGamePaused -= GamePaused;
        EventService.Instance.OnGameResumed -= GameResumed;
    }

    private void Start()
    {
        EventService.Instance.OnGameOver += GameOver;
        EventService.Instance.OnGamePaused += GamePaused;
        EventService.Instance.OnGameResumed += GameResumed;
        SpawnWave();
    }

    public void SpawnWave()
    {
        if (!b_IsGameOver && b_IsWaveCompleted)
        {
            b_IsWaveCompleted = false;
            currentWave++;

            EventService.Instance.InvokeOnWaveSurvivedEvent();

            UIHandler.Instance.ShowDisplayText("Wave " + currentWave.ToString(), 3f);
            float enemiesToBeSpawned = Mathf.Pow(2, (currentWave - 1));

            SpawnEnemy(enemiesToBeSpawned);
        }
    }

    public async void SpawnEnemy(float enemyCount)
    {
        for(int i=0; i < enemyCount; i++)
        {
            await new WaitForSeconds(tankSpawnDelay + 1);

            if(EnemyTankService.Instance.enemyTanks.Count > 2 || b_IsGamePaused)
            {
                i--;
            }
            else
            {          
                int rand = Random.Range(0, EnemyTankService.Instance.enemyTankList.enemies.Length);
                EnemyTankService.Instance.CreateEnemyTank((EnemyType)rand);
            }
        }

        b_IsWaveCompleted = true;
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }

    private void GameOver()
    {
        b_IsGameOver = true;
    }

    private void GamePaused()
    {
        b_IsGamePaused = true;
    }

    private void GameResumed()
    {
        b_IsGamePaused = false;
    }
}
