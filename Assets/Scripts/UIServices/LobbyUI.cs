using GameplayServices;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIServices
{
    public class LobbyUI : MonoBehaviour
    {
        public Image backgroundImage;
        public GameObject buttons;

        public void Play()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void HighScore()
        {
            //
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
