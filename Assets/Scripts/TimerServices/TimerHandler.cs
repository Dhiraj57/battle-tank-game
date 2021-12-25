using GlobalServices;
using UnityEngine;

namespace TimerServices
{
    public class TimerHandler : MonoSingletonGeneric<TimerHandler>
    {
        public void SetLowTimeScaleValue()
        {
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        public void SetHighTimeScaleValue()
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }
}
