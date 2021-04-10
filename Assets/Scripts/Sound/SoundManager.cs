using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour
{
    public static SoundManager Instance;
    public GameObject SoundSource;
    private AudioSource _musicSource;
    private Sound _themeSong;
    private AudioClip _nextMusicClip;

    public enum Sound
    {
        upbeatMusic,
        happyMusic,
        calmMusic,
        sadMusic
    }

    private void Awake()
    {
        Instance = this;
        _musicSource = GetComponent<AudioSource>();
        Events.OnHourPassed += SetThemeSong;
    }

    private void Start()
    {
        _musicSource.loop = true;
        _themeSong = Sound.happyMusic;
    }

    private void OnDestroy()
    {
        Events.OnHourPassed -= SetThemeSong;
    }

    public void PlaySound(Sound sound)
    {
        GameManager.SoundAudioClip clip = GetAudioClip(sound);
        GameObject newSound = Instantiate(SoundSource, transform.position, Quaternion.identity);
        newSound.GetComponent<SoundPrefab>().clip = clip;
    }

    public void PlaySound(string sound)
    {
        PlaySound((Sound)System.Enum.Parse(typeof(Sound), sound));
    }

    private static GameManager.SoundAudioClip GetAudioClip(Sound sound)
    {
        foreach (GameManager.SoundAudioClip soundAudioClip in GameManager.Instance.AllGameSounds)
        {
            if(soundAudioClip.sound == sound)
            {
                return soundAudioClip;
            }
        }

        return null;
    }

    private void SetThemeSong()
    {
        float stress = GameManager.Instance.GetStress();
        AudioClip newSong;

        if (stress > 75f)
        {
            newSong = GetAudioClip(Sound.sadMusic).Clip;
        }
        else if (stress > 50f)
        {
            newSong = GetAudioClip(Sound.calmMusic).Clip;
        }
        else if (stress > 25f)
        {
            newSong = GetAudioClip(Sound.happyMusic).Clip;
        }
        else
        {
            newSong = GetAudioClip(Sound.upbeatMusic).Clip;
        }

        if (_musicSource.clip != newSong)
        {
            ChangeMusicClip(newSong);
        }
    }

    public void PlayThemeSong()
    {
        _musicSource.clip = GetAudioClip(_themeSong).Clip;
        _musicSource.Play();
    }

    public void PlayNextClip()
    {
        _musicSource.clip = _nextMusicClip;
        _musicSource.Play();
    }

    public void ChangeMusicClip(AudioClip musicClip)
    {
        _nextMusicClip = musicClip;

        float fadeOutDuration = 2.0f;
        float fadeOutvolume = 0.0f;
        float fadeInDuration = 1.0f;
        float fadeInVolume = _musicSource.volume;

        StartCoroutine(MusicFadeOutAndNextClipFadeIn(fadeOutDuration, fadeOutvolume, fadeInDuration, fadeInVolume));
    }

    public IEnumerator MusicFadeOutAndNextClipFadeIn(float fadeOutDuration, float fadeOutVolume, float fadeInDuration, float fadeInVolume)
    {
        float currentTime = 0;
        float start = _musicSource.volume;

        // Fades out music
        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(start, fadeOutVolume, currentTime / fadeOutDuration);
            yield return null;
        }

        // Start playing next clip
        PlayNextClip();

        // Fades in music
        currentTime = 0;
        start = _musicSource.volume;

        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(start, fadeInVolume, currentTime / fadeInDuration);
            yield return null;
        }

        yield break;
    }

    public IEnumerator MusicFadeOut(float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = _musicSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            _musicSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }

        yield break;
    }
}
