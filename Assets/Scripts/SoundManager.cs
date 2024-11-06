using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource soundManagerSource;
    public AudioSource musicManagerSource;
    private AudioSource[] audioSources;

    public AudioClip BossDamage;
    public AudioClip BossDie;
    public AudioClip BossShoot;
    public AudioClip[] Keyboard;
    public AudioClip PlayerDamage;
    public AudioClip PoisonDie;
    public AudioClip SmokeDie;

    public AudioClip BGMusic;
    public AudioClip GameplayMusic;


    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSources = GetComponentsInChildren<AudioSource>();
            soundManagerSource = audioSources[0];
            musicManagerSource = audioSources[1];

        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlaySound(string name)
    {
        switch (name)
        {
            case "BossDamage":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(BossDamage);
                break;

            case "BossDie":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(BossDie);
                break;

            case "Keyboard":
                soundManagerSource.volume = 0.20f;
                int number = Random.Range(0, 1);
                soundManagerSource.PlayOneShot(Keyboard[number]);
                break;

            case "PlayerDamage":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(PlayerDamage);
                break;

            case "PoisonDie":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(PoisonDie);
                break;

            case "SmokeDie":
                soundManagerSource.volume = 0.30f;

                soundManagerSource.PlayOneShot(SmokeDie);
                break;

           
        }
    }

    public void PlayMusic()
    {
        musicManagerSource.loop = true;
        musicManagerSource.Play();
    }

    public void ChangeMusic(string name)
    {

        switch (name)
        {
            case "BG":
                Debug.Log(musicManagerSource);

                if (musicManagerSource.clip != BGMusic)
                {
                    musicManagerSource.clip = BGMusic;
                    PlayMusic();
                }
           
                break;

            case "Gameplay":

                if (musicManagerSource.clip != GameplayMusic)
                {
                    musicManagerSource.clip = GameplayMusic;
                    PlayMusic();
                }
                break;


        }
    }

}
