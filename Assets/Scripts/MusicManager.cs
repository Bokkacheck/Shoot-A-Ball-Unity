using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static MusicManager singleton;
    public AudioSource music;
    public Slider sliderMusic;
    public Slider sliderGame;

    public float gameVolume;
    void Start()
    {
        if (singleton == null)
        {
            singleton = this;
            music.volume = PlayerPrefs.GetFloat("MusicVolume",0.5f);
            gameVolume = PlayerPrefs.GetFloat("GameVolume", 1f);
            sliderGame.value = gameVolume;
            DontDestroyOnLoad(gameObject);
        }
        if(this != singleton)
        {
            singleton.sliderGame = sliderGame;
            singleton.sliderMusic = sliderMusic;
            Destroy(gameObject);
        }
        singleton.sliderMusic.value = singleton.music.volume;
        singleton.sliderGame.value = singleton.gameVolume;
    }
    public void HandleMusic()
    {
        singleton.music.enabled = !singleton.music.enabled;
    }
}
