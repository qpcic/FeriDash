using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance; // Singleton vzorec
    private AudioSource audioSource;

    [SerializeField] private AudioClip menuMusic; // Menu glasba
    [SerializeField] private List<LevelMusic> levelMusicClips; // Ingame glasba

    // Razred za povezavo imena scene z glasbo
    [System.Serializable]
    private class LevelMusic
    {
        public string sceneName; // Ime scene (npr. "Level1", "Level3")
        public AudioClip musicClip; // Glasba za to sceno
    }

    private void Awake()
    {
        // Singleton: zagotovi, da obstaja samo ena instanca MusicManagerja
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ohrani objekt med scenami
        }
        else
        {
            Destroy(gameObject); // Uniči dodatne instance
        }

        audioSource = GetComponent<AudioSource>();
        LoadVolume(); // Naloži shranjeno glasnost
        PlayMusicBasedOnScene(SceneManager.GetActiveScene().name); // Predvajaj glasbo glede na sceno
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // Naroči se na dogodek ob nalaganju scene
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Odjavi se od dogodka
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicBasedOnScene(scene.name); // Predvajaj ustrezno glasbo ob nalaganju scene
    }

    private void PlayMusicBasedOnScene(string sceneName)
    {
        // Seznam imen menu scen
        string[] menuScenes = { "MainMenu", "ProfileMenu", "LevelSelect", "SettingsMenu"};

        // Preveri, ali je trenutna scena menu scena
        if (System.Array.Exists(menuScenes, menu => menu == sceneName))
        {
            if (audioSource.clip != menuMusic)
            {
                audioSource.clip = menuMusic;
                audioSource.Play();
            }
        }
        else
        {
            // Preveri, ali obstaja glasba za to sceno v seznamu nivojev
            foreach (var levelMusic in levelMusicClips)
            {
                if (levelMusic.sceneName == sceneName && levelMusic.musicClip != null)
                {
                    if (audioSource.clip != levelMusic.musicClip)
                    {
                        audioSource.clip = levelMusic.musicClip;
                        audioSource.Play();
                    } else {
                        audioSource.Play();
                    }
                        return;
                }
            }

            if (sceneName == "LevelPause") {
                audioSource.Stop();
            }

            if (sceneName == "MainMenu") // Privzeto uporabi menu glasbo, če ni najdenega ujemanja
            {
                audioSource.clip = menuMusic;
                audioSource.Play();
            }
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume); // Shrani glasnost
        PlayerPrefs.Save();
    }

    private void LoadVolume()
    {
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", 0.6f); // Privzeto 1, če ni shranjeno
        audioSource.volume = savedVolume;
    }
}