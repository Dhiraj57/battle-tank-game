using AchievementSO;
using GlobalServices;
using PlayerTankServices;
using UIServices;
using UnityEngine;

namespace AchievementServices
{
    public class AchievementHandler : MonoSingletonGeneric<AchievementHandler>
    {
        [SerializeField] private AchievementHolder achievementSOList;

        private int currentBulletFiredAchivementLevel;
        private int currentEnemiesKilledAchievementLevel;
        private int currentWavesSurvivedAchievementLevel;

        private void Start()
        {
            currentBulletFiredAchivementLevel = PlayerPrefs.GetInt("currentBulletFiredAchivementLevel", 0);
            currentEnemiesKilledAchievementLevel = PlayerPrefs.GetInt("currentEnemiesKilledAchievementLevel", 0);
            currentWavesSurvivedAchievementLevel = PlayerPrefs.GetInt("currentWavesSurvivedAchievementLevel", 0);
        }

        public void CheckForBulletFiredAchievement()
        {
            for(int i=0; i < achievementSOList.bulletsFiredAchievementSO.achievements.Length; i++)
            {
                if (i != currentBulletFiredAchivementLevel) continue;

                if(PlayerTankService.Instance.GetTankController().tankModel.bulletsFired == achievementSOList.bulletsFiredAchievementSO.achievements[i].requirement)
                {
                    UnlockAchievement(achievementSOList.bulletsFiredAchievementSO.achievements[i].name, achievementSOList.bulletsFiredAchievementSO.achievements[i].info);
                    currentBulletFiredAchivementLevel = i + 1;
                    PlayerPrefs.SetInt("currentBulletFiredAchivementLevel", currentBulletFiredAchivementLevel);
                }
                break;
            }
        }

        public void CheckForEnemiesKilledAchievement()
        {
            for(int i=0; i < achievementSOList.enemiesKilledAchievementSO.achievements.Length; i++)
            {
                if (i != currentEnemiesKilledAchievementLevel) continue;

                if(PlayerTankService.Instance.GetTankController().tankModel.enemiesKilled == achievementSOList.enemiesKilledAchievementSO.achievements[i].requirement)
                {
                    UnlockAchievement(achievementSOList.enemiesKilledAchievementSO.achievements[i].name, achievementSOList.enemiesKilledAchievementSO.achievements[i].info);
                    currentEnemiesKilledAchievementLevel = i + 1;
                    PlayerPrefs.SetInt("currentEnemiesKilledAchievementLevel", currentEnemiesKilledAchievementLevel);
                }
                break;
            }
        }

        public void CheckForWavesSurvivedAvhievement()
        {
            for(int i=0; i < achievementSOList.waveSurvivedAchievementSO.achievements.Length; i++)
            {
                if (i != currentWavesSurvivedAchievementLevel) continue;

                if(PlayerTankService.Instance.GetTankController().tankModel.waveSurvived == achievementSOList.waveSurvivedAchievementSO.achievements[i].requirement)
                {
                    UnlockAchievement(achievementSOList.waveSurvivedAchievementSO.achievements[i].name, achievementSOList.waveSurvivedAchievementSO.achievements[i].info);
                    currentWavesSurvivedAchievementLevel = i + 1;
                    PlayerPrefs.SetInt("currentWavesSurvivedAchievementLevel", currentWavesSurvivedAchievementLevel);
                }
                break;
            }
        }

        private void UnlockAchievement(string achievementName, string achievementInfo)
        {
            UIHandler.Instance.ShowAchievementUnlocked(achievementName, achievementInfo, 3f);
        }
    }
}
