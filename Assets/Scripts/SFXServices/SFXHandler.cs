using GlobalServices;
using UnityEngine;

namespace SFXServices
{
    public class SFXHandler : MonoSingletonGeneric<SFXHandler>
    {
        public AudioSource audioSource1;
        public AudioSource audioSource2;
        public AudioSource enemyTrack;

        // For fire / destruction sound
        public void PlayFireSoundTrack(AudioClip audioClip, float volume, int priority, bool overrideSound = true)
        {
            if ((audioSource1.clip != audioClip) || (audioSource1.isPlaying && overrideSound))
            {
                audioSource1.clip = audioClip;
                audioSource1.volume = volume;
                audioSource1.priority = priority;
                audioSource1.Play();
            }
            else return;
        }

        public void PlayMovingSoundTrack(AudioClip audioClip, float volume, int priority, bool overrideSound = true)
        {
            if ((audioSource2.clip != audioClip) || (audioSource2.isPlaying && overrideSound))
            {
                audioSource2.clip = audioClip;
                audioSource2.volume = volume;
                audioSource2.priority = priority;
                audioSource2.Play();
            }
            else return;
        }

        public void PlayEnemySoundTrack(AudioClip audioClip, float volume, int priority, bool overrideSound = true)
        {
            if ((enemyTrack.clip != audioClip) || (enemyTrack.isPlaying && overrideSound))
            {
                enemyTrack.clip = audioClip;
                enemyTrack.volume = volume;
                enemyTrack.priority = priority;
                enemyTrack.Play();
            }
            else return;
        }

        public void TurnOffSoundsExceptUI()
        {
            audioSource1.enabled = false;
            audioSource2.enabled = false;
            enemyTrack.enabled = false;
        }

        public void TurnOnSounds()
        {
            audioSource1.enabled = true;
            audioSource2.enabled = true;
            enemyTrack.enabled = true;
        }
    }
}
