using System;
using UnityEngine;

namespace Tanks
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        public Sound[] musicSounds, sfxSounds, moveSounds;
        public AudioSource musicSource, sfxSource, moveSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            PlayMusic("MainTheme");
        }

        public void PlayMusic(string name)
        {
            Sound sound = Array.Find(musicSounds, x => x._name == name);

            if (sound == null)
            {
                Debug.Log("Not Found Sound");
            }
            else
            {
                musicSource.clip = sound._clip;
                musicSource.Play();
            }
        }

        public void PlaySFX(string name)
        {
            {
                Sound sound = Array.Find(sfxSounds, x => x._name == name);

                if (sound == null)
                {
                    Debug.Log("Not Found Sound");
                }
                else
                {
                    sfxSource.PlayOneShot(sound._clip);
                }
            }
        }
        public void PlayOnMove(string name)
        {
            Sound sound = Array.Find(moveSounds, x => x._name == name);
            if (sound == null)
            {
                Debug.Log("Not Found Sound");
            }
            else
            {
                moveSource.clip = sound._clip;
                moveSource.Play();
            }

        }
        public void StopOnMove()
        {
            moveSource.Stop();
        }
    }
}