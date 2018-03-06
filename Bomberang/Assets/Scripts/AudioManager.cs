using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioSource tickAudio;
    public AudioClip[] audioArr;
    public AudioClip throwClip;
    public GameObject bomberang;
    public AudioMixerGroup audmixMixer;
    public GameObject goSound;
    public float pitchMultiplier;


    // Use this for initialization
    void Start()
    {
        pitchMultiplier = pitchMultiplier * 0.001f;
        tickAudio.clip = audioArr[0];
        tickAudio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(tickAudio.pitch);
        if (bomberang.GetComponent<Bomberang>().timer <= 5 && bomberang.GetComponent<Bomberang>().timer > 0)
        {
            tickAudio.pitch += pitchMultiplier;
        }

        if (bomberang.GetComponent<Bomberang>().timer <= 0f && bomberang.GetComponent<Bomberang>().isHeld)
        {
            tickAudio.pitch = 1;
            //tickAudio.clip = null;
            tickAudio.clip = audioArr[1];
            if (!tickAudio.isPlaying && tickAudio.clip == audioArr[0])
            {
                tickAudio.Play();
            }

            if (!tickAudio.isPlaying && tickAudio.clip == audioArr[1])
            {
                tickAudio.clip = null;
                
            }
            //play BOOM
        }
    }

    public void PlayThrowSound()
    {
        AudioSource sfxSource = new AudioSource();
        sfxSource.clip = throwClip;
        sfxSource.Play();
        sfxSource.pitch = Random.Range(0.9f, 1.1f);
    }

    
}
