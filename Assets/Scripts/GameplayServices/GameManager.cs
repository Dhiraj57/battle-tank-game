using GlobalServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using SFXServices;
using EnemyTankServices;
using PlayerTankServices;
using UIServices;

namespace GameplayServices
{
    public class GameManager : MonoSingletonGeneric<GameManager>
    {
        [HideInInspector] public bool gamePaused = false;
        [HideInInspector] public bool gameOver = false;
        private string currentPlayerName;
        private string recordHolderName;
        private int highScore;
        private int currentWave;

        private void Start()
        {
            currentWave = 0;
            highScore = PlayerPrefs.GetInt("highScore", PlayerPrefs.GetInt("highScore"));
            recordHolderName = PlayerPrefs.GetString("recordHolderName", PlayerPrefs.GetString("recordHolderName"));
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        public void PasueGame()
        {
            gamePaused = true;
            SFXHandler.Instance.TurnOffSoundsExceptUI();
            PlayerTankService.Instance.TurnOFFTanks();
            EnemyTankService.Instance.TurnOFFEnemies();
        }

        public void ResumeGame()
        {
            gamePaused = false;
            SFXHandler.Instance.TurnOnSounds();
            PlayerTankService.Instance.TurnONTanks();
            EnemyTankService.Instance.TurnONEnemies();
        }

        public void SetCurrentPlayerName(string name)
        {
            currentPlayerName = name;
            PlayerPrefs.SetString("currentPlayerName", currentPlayerName);
        }

        public void CheckForHighScore()
        {
            if(UIHandler.Instance.GetCurrentScore() > highScore)
            {
                PlayerPrefs.SetInt("highScore", UIHandler.Instance.GetCurrentScore());
                PlayerPrefs.SetString("recordHolderName", currentPlayerName);

                recordHolderName = currentPlayerName;
                highScore = UIHandler.Instance.GetCurrentScore();
            }
        }

        public string GetHighScore()
        {
            return PlayerPrefs.GetInt("highScore").ToString();
        }

        public string GetRecordHolderName()
        {
            return PlayerPrefs.GetString("recordHolderName");
        }
    }
}
