using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] public AudioSource musicSource, sfxSource;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("GameMusic");
    }

    

    public void PlayMusic(string name)
    {
        Sound ses = Array.Find(musicSounds, x => x.name == name);



        if (ses == null)
        {
            Debug.Log("Ses bulunamadý.");
        }
        else
        {
            musicSource.clip = ses.clip;
            if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 2)
            {
                musicSource.pitch = 1f;
                musicSource.Play();
            }
            else
            {
                musicSource.pitch = 2f;
                musicSource.Play();
            }
            
        }
    }

    public void PlaySFX(string name)
    {
        Sound ses = Array.Find(sfxSounds, x => x.name == name);

        if (ses == null)
        {
            Debug.Log("Ses bulunamadý.");
        }
        else
        {
            sfxSource.PlayOneShot(ses.clip);
        }
    }
}
