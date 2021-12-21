using GameplayServices;
using GlobalServices;
using UnityEngine;
using UnityEngine.UI;

namespace UIServices
{ 
    public class UIHandler : MonoSingletonGeneric<UIHandler>
    {
        [SerializeField] private GameObject joystickControllerObject;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject gameOverPanel;

        [SerializeField] private Image achievementImage;

        [SerializeField] private Text achievementText;
        [SerializeField] private Text achievementNameText;
        [SerializeField] private Text achievementInfoText;
        [SerializeField] private Text displayText;
        [SerializeField] private Text scoreText;

        private int currentScore;

        private void Start()
        {
            currentScore = 0;
            scoreText.text = "Score : " + currentScore.ToString();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape) && !GameManager.Instance.gamePaused)
            {
                GameManager.Instance.PasueGame();
                pausePanel.SetActive(true);
            }
            else if (GameManager.Instance.gamePaused && Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.Instance.ResumeGame();
                pausePanel.SetActive(true);
            }
        }

        public async void ShowAchievementUnlocked(string name, string achievementInfo, float timeForDisplay)
        {
            GameManager.Instance.PasueGame();
            achievementText.text = "ACHIEVEMENT UNLOCKED";
            achievementNameText.text = name;
            achievementInfoText.text = achievementInfo;
            achievementImage.gameObject.SetActive(true);
            await new WaitForSeconds(timeForDisplay);
            achievementText.text = null;
            achievementNameText.text = null;
            achievementInfoText.text = null;
            achievementImage.gameObject.SetActive(false);
            GameManager.Instance.ResumeGame();
        }

        public void ShowGameOverUI()
        {
            GameManager.Instance.PasueGame();
            gameOverPanel.SetActive(true);
        }

        public async void ShowDisplayText(string text, float timeForDisplay)
        {
            UpdateDisplayText(text);
            GameManager.Instance.PasueGame();
            displayText.gameObject.SetActive(true);
            await new WaitForSeconds(timeForDisplay);
            displayText.gameObject.SetActive(false);
            GameManager.Instance.ResumeGame();
        }

        public int GetCurrentScore()
        {
            return currentScore;
        }

        public void UpdateScoreText(int scoreMultiplier = 1)
        {
            int finalScore = (currentScore + 10) * scoreMultiplier;
            currentScore = finalScore;
            scoreText.text = "Score : " + finalScore.ToString();
        }

        public void ResetScore()
        {
            currentScore = 0;
            scoreText.text = "Score : " + currentScore.ToString();
        }

        public void UpdateDisplayText(string text)
        {
            displayText.text = text;
        }
    }
}

