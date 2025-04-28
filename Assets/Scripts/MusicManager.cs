using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource UISource;


    [SerializeField] AudioClip buttonClickSound;
    [SerializeField] AudioClip feedingSound;
    [SerializeField] AudioClip raidSound;
    [SerializeField] AudioClip newWarriorSound;
    [SerializeField] AudioClip newCitizenSound;
    [SerializeField] AudioClip harvestingSound;

    [SerializeField] AudioClip backgroundMusic;
    [SerializeField] AudioClip mainMenuBackgroundMusic;
    [SerializeField] AudioClip winMusic;
    [SerializeField] AudioClip looseMusic;

    private float currentVolume = 0.5f;
    private float savedTimePreviousMusic = 0;

    public static MusicManager instance;

    private MusicManager() { }

    public static MusicManager GetInstance() {  return instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        musicSource.volume = currentVolume;
        musicSource.loop = true;
        musicSource.clip = mainMenuBackgroundMusic;
        musicSource.Play();

        UISource.volume = currentVolume;
    }

    internal void ChangeVolume(float newValue)
    {
        Debug.Log($"new volume = {newValue}");
        currentVolume = newValue;
        musicSource.volume = newValue;
        UISource.volume = newValue;
    }

    internal void PauseGameSounds()
    {
        savedTimePreviousMusic = musicSource.time;

        musicSource.clip = mainMenuBackgroundMusic;
        musicSource.Play();

    }

    internal void StartGameSounds()
    {

        musicSource.clip = backgroundMusic;
        musicSource.time = savedTimePreviousMusic;
        musicSource.Play();

    }

    internal void MainMenuSounds()
    {
        if (musicSource.clip != mainMenuBackgroundMusic)
        {
            musicSource.clip = mainMenuBackgroundMusic;
            musicSource.Play();
        }
    }

    internal void GameLostSounds()
    {
        UISource.Stop();
        UISource.Play();

        musicSource.clip = looseMusic;
        musicSource.Play();
    }

    internal void GameWinSounds()
    {
        musicSource.clip = winMusic;
        musicSource.Play();
    }


    internal void SoundOnButtonClick()
    {
        UISource.PlayOneShot(buttonClickSound);
    }

    internal void FeedingTimeSound()
    {
        UISource.PlayOneShot(feedingSound);
    }

    internal void RaidTimeSound()
    {
        UISource.PlayOneShot(raidSound);
    }

    internal void NewWarriorSound()
    {
        UISource.PlayOneShot(newWarriorSound);
    }

    internal void HarvestingTimeSound()
    {
        UISource.PlayOneShot(harvestingSound);
    }

    internal void NewCitizenSound()
    {
        UISource.PlayOneShot(newCitizenSound);
    }


}
