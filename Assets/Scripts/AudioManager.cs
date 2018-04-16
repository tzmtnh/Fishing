using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AudioManager : MonoBehaviour {

    public float defaultMusicVolume = 0.3f;

	[System.Serializable]
	public class AudioElement {
		public string name;
		public float defaultVolume = 1.0f;
		public float defaultPitch = 1.0f;
		public AudioClip clip;
	}

    AudioSource musicPlaying;

	public static AudioManager inst;

	public AudioElement[] audioElements;

	Stack<AudioSource> _freeSources = new Stack<AudioSource>(8);
	List<AudioSource> _activeSources = new List<AudioSource>(8);
	Dictionary<string, AudioElement> _elementByName = new Dictionary<string, AudioElement>();

	AudioSource getSource() {
		AudioSource source;
		if (_freeSources.Count > 0) {
			source = _freeSources.Pop();
		} else {
			GameObject obj = new GameObject();
			obj.transform.SetParent(transform);
			source = obj.AddComponent<AudioSource>();
		}

		_activeSources.Add(source);
		return source;
	}

	public AudioSource playSound(string name, float volume = -1, float pitch = -1, bool loop = false) {
		if (_elementByName.ContainsKey(name) == false) {
			Debug.LogError("Unable to locate sound " + name);
			return null;
		}

		AudioElement element = _elementByName[name];
		AudioSource source = getSource();

		if (volume < 0) {
			volume = element.defaultVolume;
		}
		if (pitch < 0) {
			pitch = element.defaultPitch;
		}

		source.clip = element.clip;
		source.volume = volume;
		source.pitch = pitch;
		source.loop = loop;
		source.name = "Audio Source - " + name;
		source.Play();
		return source;
	}

	/*
	public void stopAllSounds() {
		foreach (AudioSource source in _activeSources) {
			source.Stop();
			source.clip = null;
			_freeSources.Push(source);
		}
		_activeSources.Clear();
	}
	*/

	public void pauseAllSounds(bool pause) {
		foreach (AudioSource source in _activeSources) {
			if (pause)
				source.Pause();
			else
				source.UnPause();
		}
	}

	public void stopSound(AudioSource source) {
        if (source == null || source.isPlaying == false)
			return;

		Assert.IsTrue(_activeSources.Contains(source));
		source.Stop();
		source.clip = null;
		_activeSources.Remove(source);
		_freeSources.Push(source);
	}

	void Awake() {
		Assert.IsNull(inst, "There can be only one instance!");
		inst = this;

		foreach (AudioElement element in audioElements) {
			_elementByName.Add(element.name, element);
		}
	}


	void Update() {
		if (Time.timeScale == 0) return;

		// free un-playing sounds
		for (int i = _activeSources.Count - 1; i >= 0; i--) {
			AudioSource source = _activeSources[i];
			if (source.isPlaying == false) {
                source.clip = null;
                _freeSources.Push(source);
                _activeSources.RemoveAt(i);
			}
		}
	}

    public void playThemeMusic() {
        stopSound(musicPlaying);
        musicPlaying = playSound("Boat_Theme_Intro", defaultMusicVolume, 1, false);
        Invoke("playThemeMusicLoop", musicPlaying.clip.length);

    }

    public void playThemeMusicLoop()
    {
        if (GameManager.inst.state == GameManager.GameState.StartMenu ||
            GameManager.inst.state == GameManager.GameState.EndGame)
        {
            stopSound(musicPlaying);
            musicPlaying = playSound("Boat_Theme_Loop", defaultMusicVolume, 1, true);
        }
    }

    public void playFishingMusic(bool reverse = false)
    {
        stopSound(musicPlaying);
        if (reverse) {
            musicPlaying = playSound("Fishing_Music_Reverse", defaultMusicVolume, 1, true); 
        } else {
            musicPlaying = playSound("Fishing_Music", defaultMusicVolume, 1, true);
        }

    }

    public void playNinjaMusic() {
        stopSound(musicPlaying);
        musicPlaying = playSound("Ninja_Music_Intro", defaultMusicVolume, 1, false);
        Invoke("playNinjaMusicLoop", musicPlaying.clip.length);
    }

    public void playNinjaMusicLoop()
    {
        if (GameManager.inst.state == GameManager.GameState.Ninja)
        {
            stopSound(musicPlaying);
            musicPlaying = playSound("Ninja_Music", defaultMusicVolume, 1, true);
        }
    }
}
