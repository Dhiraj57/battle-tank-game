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

        [SerializeField] private GameObject buttons;
        [SerializeField] private Image achievementImage;

        [SerializeField] private Text achievementText;
        [SerializeField] private Text achievementNameText;
        [SerializeField] private Text achievementInfoText;
        [SerializeField] private Text displayText;
        [SerializeField] private Text scoreText;

        private int currentScore;
        private bool b_IsGameOver;
        private bool b_IsAchievementVisible;

        private void Start()
        {
            currentScore = 0;
            b_IsGameOver = false;
            b_IsAchievementVisible = false;

            scoreText.text = "Score : " + currentScore.ToString();

            EventService.Instance.OnGameOver += GameOver;
            EventService.Instance.OnGamePaused += GamePaused;
            EventService.Instance.OnGameResumed += GameResumed;
        }

        private void OnDisable()
        {
            EventService.Instance.OnGameOver -= GameOver;
            EventService.Instance.OnGamePaused -= GamePaused;
            EventService.Instance.OnGameResumed -= GameResumed;
        }

        private void GamePaused()
        {
            scoreText.gameObject.SetActive(false);
            joystickControllerObject.SetActive(false);
            buttons.gameObject.SetActive(false);
            pausePanel.gameObject.SetActive(true);
        }

        private void GameResumed()
        {
            scoreText.gameObject.SetActive(true);
            joystickControllerObject.SetActive(true);
            buttons.gameObject.SetActive(true);
            pausePanel.gameObject.SetActive(false);
        }

        public async void ShowAchievementUnlocked(string name, string achievementInfo, float timeForDisplay)
        {
            b_IsAchievementVisible = true;

            GameManager.Instance.PasueGame();
            GamePaused();
            pausePanel.gameObject.SetActive(false);
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
            GameResumed();

            b_IsAchievementVisible = false;
        }

        public void ShowGameOverUI()
        {
            gameOverPanel.SetActive(true);
            pausePanel.SetActive(false);
            SetGameOverPanelAlpha();
        }

        public async void ShowDisplayText(string text, float timeForDisplay)
        {
            if(b_IsAchievementVisible)
            {
                await new WaitForSeconds(timeForDisplay);
            }

            UpdateDisplayText(text);
            GameManager.Instance.PasueGame();
            GamePaused();
            pausePanel.gameObject.SetActive(false);
            displayText.gameObject.SetActive(true);

            await new WaitForSeconds(timeForDisplay);

            displayText.gameObject.SetActive(false);
            GameManager.Instance.ResumeGame();
            GameResumed();
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

        async private void GameOver()
        {
            scoreText.gameObject.SetActive(false);
            joystickControllerObject.SetActive(false);
            buttons.gameObject.SetActive(false);

            await new WaitForSeconds(4.5f);
            b_IsGameOver = true;
            ShowGameOverUI();
        }

        async private void SetGameOverPanelAlpha()
        {
            float newAlpha = 0;
            Color panelColor = gameOverPanel.GetComponent<Image>().color;

            while (newAlpha < 1)
            {
                newAlpha += Time.deltaTime;
                newAlpha = Mathf.Min(newAlpha, 1f);

                panelColor.a = newAlpha;
                gameOverPanel.GetComponent<Image>().color = panelColor;

                await new WaitForSeconds(0.0005f);
            }
        }
    }
}

