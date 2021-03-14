using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPrefab : MonoBehaviour
{
    public GameManager.SoundAudioClip clip;
    private AudioSource src;
    private float startTime;

    private void Awake()
    {
        src = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        src.outputAudioMixerGroup = clip.mixerGroup;
        startTime = Time.time;
        src.PlayOneShot(clip.Clip);
    }

    private void Update()
    {
        if (startTime + clip.Clip.length < Time.time)
        {
            Destroy(gameObject);
        }
    }
}
