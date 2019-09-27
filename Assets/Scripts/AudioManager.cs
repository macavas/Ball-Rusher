using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;

    public static AudioManager instance;

	// Use this for initialization
	void Awake () {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

		foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
	}

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        if(PlayerPrefs.GetInt("Music" ,1) == 1)
        {
            Sound s = Array.Find(sounds, sound => sound.name == "GameMusic");
            if(s == null)
            {
                Debug.Log("Sound: GameMusic Could not found.");
                return;
            }
            s.source.Play();
        }
    }

    public void StopMusic()
    {
        
            Sound s = Array.Find(sounds, sound => sound.name == "GameMusic");
            if (s == null)
            {
                Debug.Log("Sound: GameMusic Could not found.");
                return;
            }
            s.source.Stop();
    }

    public void Play(string name)
    {
        if(PlayerPrefs.GetInt("Sound") == 1)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
            {
                Debug.Log("Sound: " + name + " Could not found.");
                return;
            }
            s.source.Play();
        }
    }

}
