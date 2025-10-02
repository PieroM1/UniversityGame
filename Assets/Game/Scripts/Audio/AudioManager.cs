using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Music and SFX")]
    [SerializeField] private List<AudioTrack> musicTracks;
    private Dictionary<AudioConstants, AudioClip> musicDict;

    [SerializeField] private List<SFXTrack> soundEffects;
    private Dictionary<SFXConstants, AudioClip> sfxDict;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        //Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BuildDictionaries();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void BuildDictionaries()
    {
        musicDict = new Dictionary<AudioConstants, AudioClip>();
        foreach (var track in musicTracks)
        {
            musicDict[track.key] = track.clip;
        }
        sfxDict = new Dictionary<SFXConstants, AudioClip>();
        foreach (var sfx in soundEffects)
        {
            sfxDict[sfx.key] = sfx.clip;
        }
    }

    public void ChangeBackgroundMusic(AudioConstants backgroundName)
    {
        if (musicDict.ContainsKey(backgroundName))
        {
            musicSource.clip = musicDict[backgroundName];
            musicSource.Play();
        }
    }

    public IEnumerator Crossfade(AudioClip newClip, float duration = 1f)
    {
        float startVolume = musicSource.volume;

        // Fade out
        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.clip = newClip;
        musicSource.Play();

        // Fade in
        while (musicSource.volume < startVolume)
        {
            musicSource.volume += startVolume * Time.deltaTime / duration;
            yield return null;
        }
    }

    public void StopBackgroundMusic() => musicSource.Stop();

    public void PlaySFX(SFXConstants key)
    {
        if (sfxDict.ContainsKey(key))
        {
            Debug.Log("Playing SFX: " + key);
            sfxSource.PlayOneShot(sfxDict[key]);
        }
    }
}