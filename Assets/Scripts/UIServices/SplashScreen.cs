using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UIServices
{ 
    public class SplashScreen : MonoBehaviour
    {
        public Image image;

        async void Start()
        {
            await new WaitForSeconds(3f);

            float newAlpha = 1;
            Color panelColor = image.color;

            while (newAlpha > 0)
            {
                newAlpha -= Time.deltaTime;
                newAlpha = Mathf.Max(newAlpha, 0f);

                panelColor.a = newAlpha;
                image.color = panelColor;

                await new WaitForSeconds(0.001f);
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private void Update()
        {
            
        }
    }
}
