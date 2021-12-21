using System;
using UnityEngine;

namespace AchievementSO
{
    [CreateAssetMenu(fileName = "WaveSurvivedAchievementSO", menuName = "ScriptableObject/Achievement/NewWaveSurvivedAchievementSO")]
    public class WaveSurvivedAchievementSO : ScriptableObject
    {
        public AchievementType[] achievements;

        [Serializable]
        public class AchievementType
        {
            public enum WaveAchievements
            {
                None,
                BulletProof,
                SteelForge,
                Survivor
            }

            public string name;
            public string info;
            public WaveAchievements selectAchievement; 
            public int requirement;
        }
    }
}