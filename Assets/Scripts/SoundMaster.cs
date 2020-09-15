using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaster : MonoBehaviour
{
    public static SoundMaster _instance;

    public float effectsVolume = 0.5f;

    public AudioSource songSource;
    public List<AudioClip> songs;

    private void Awake()
    {
        if (_instance)
            Destroy(this);
        else
        {
            _instance = this;
        }
    }

    public static SoundMaster GetInstance() { return _instance; }

    public void PlaySongIndex(int index)
    {
        if (index < 0 || index >= songs.Count) return;
        songSource.clip = songs[index];
        songSource.Play();
    }

    public void StopSong()
    {
        songSource.Stop();
    }

    public void PlayEffect(AudioClip clip, float pitch = 1)
    {
        AudioSource newEffect = gameObject.AddComponent<AudioSource>();
        newEffect.PlayOneShot(clip);
        newEffect.pitch = pitch;
        newEffect.volume = effectsVolume;
        Destroy(newEffect, clip.length + 0.1f);
    }

    public void SetMusicVolume(float volume)
    {
        songSource.volume = volume;
    }

    public float GetMusicVolume() { return songSource.volume; }

    public void SetEffectVolume(float volume)
    {
        effectsVolume = volume;
    }

    public float GetEffectVolume() { return effectsVolume; }
}
