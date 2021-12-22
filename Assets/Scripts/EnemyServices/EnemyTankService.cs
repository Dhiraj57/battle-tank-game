using EnemySO;
using GlobalServices;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyTankServices
{
    public class EnemyTankService : MonoSingletonGeneric<EnemyTankService>
    {
        public EnemySOList enemyTankList;
        public EnemyTankView enemyTankView;

        private List<EnemyTankController> enemyTanks = new List<EnemyTankController>();
        private EnemyType enemyTankType;

        public EnemyTankController CreateEnemyTank(EnemyType tanktype)
        {
            foreach (EnemyScriptableObject tank in enemyTankList.enemies)
            {
                if (tank.enemyType == enemyTankType)
                {
                    EnemyTankModel tankModel = new EnemyTankModel(enemyTankList.enemies[(int)tanktype]);
                    EnemyTankController tankController = new EnemyTankController(tankModel, enemyTankView);
                    enemyTanks.Add(tankController);
                    return tankController;
                }
            }
            return null;
        }

        public void DestroyEnemy(EnemyTankController enemy)
        {
            for (int i = 0; i < enemyTanks.Count; i++)
            {
                if (enemy == enemyTanks[i])
                {
                    enemyTanks[i] = null;
                    enemyTanks.Remove(enemyTanks[i]);
                }
            }

            if (enemyTanks.Count == 0)
            {
                WaveManager.Instance.SpawnWave();
            }
        }

        public void TurnOFFEnemies()
        {
            for (int i = 0; i < enemyTanks.Count; i++)
            {
                if (enemyTanks[i] != null)
                {
                    if (enemyTanks[i].tankView) enemyTanks[i].tankView.gameObject.SetActive(false);
                }
            }
        }

        public void TurnONEnemies()
        {
            for (int i = 0; i < enemyTanks.Count; i++)
            {
                if (enemyTanks[i] != null)
                {
                    if(enemyTanks[i].tankView && !enemyTanks[i].tankModel.b_IsDead) enemyTanks[i].tankView.gameObject.SetActive(true);
                }
            }
        }
    }
}
