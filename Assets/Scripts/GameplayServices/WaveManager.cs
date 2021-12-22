using EnemyTankServices;
using GameplayServices;
using GlobalServices;
using UIServices;
using UnityEngine;

public class WaveManager : MonoSingletonGeneric<WaveManager>
{
    [SerializeField] private int tankSpawnDelay = 3;

    private int currentWave = 0;

    private void Start()
    {
        SpawnWave();
    }

    public void SpawnWave()
    {
        currentWave++;
        UIHandler.Instance.ShowDisplayText("Wave " + currentWave.ToString(), 3f);
        float enemiesToBeSpawned = Mathf.Pow(2, (currentWave - 1));

        EventService.Instance.InvokeOnWaveSurvivedEvent();
        SpawnEnemy(enemiesToBeSpawned);
    }

    public async void SpawnEnemy(float enemyCount)
    {
        for(int i=0; i < enemyCount; i++)
        {
            await new WaitForSeconds(tankSpawnDelay + 1);

            int rand = Random.Range(0, EnemyTankService.Instance.enemyTankList.enemies.Length);
            EnemyTankService.Instance.CreateEnemyTank((EnemyType)rand);
        }
    }

    public int GetCurrentWave()
    {
        return currentWave;
    }
}
