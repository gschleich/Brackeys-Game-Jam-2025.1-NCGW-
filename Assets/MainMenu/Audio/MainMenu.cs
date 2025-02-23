using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider sfxSlider;
    public string nextSceneName;

    private void Start()
    {
        LoadVolume();
        MusicManager.Instance.PlayMusic("MainMenu");
    }
    public void Play()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void UpdateMusicVolume(float volume)
    {
        Debug.Log("Music Volume: " + volume);
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSoundVolume(float volume)
    {
        Debug.Log("SFX Volume: " + volume);
        audioMixer.SetFloat("SFXVolume", volume);
    }
    
    public void SaveVolume()
    {
        // Save the current volume of music and SFX
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);

        // Ensure PlayerPrefs data is written to disk
        PlayerPrefs.Save();
    }

    public void LoadVolume()
    {
        // Use a default value of 0.5f if no volume value is saved
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5f); // Default value 0.5f
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);     // Default value 0.5f
    }
}
