using GameplayServices;
using GlobalServices;
using TimerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIServices
{
    public class UIButtons : MonoBehaviour
    {
        public AudioClip click;
        public AudioSource audioSource;

        private bool b_IsTimesSlow = false;

        public void Resume()
        {
            GameManager.Instance.ResumeGame();
        }

        public void Pause()
        {
            GameManager.Instance.PasueGame();
        }

        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void Menu()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public void SlowMoButton()
        {
            if(!b_IsTimesSlow)
            {
                TimerHandler.Instance.SetLowTimeScaleValue();
                b_IsTimesSlow = !b_IsTimesSlow;
            }
            else
            {
                TimerHandler.Instance.SetHighTimeScaleValue();
                b_IsTimesSlow = !b_IsTimesSlow;
            }
        }
            
    }

}
