using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public AudioSource tickAudio;
    public AudioClip[] audioArr;
    public AudioClip throwClip;
    //public GameObject bomberang;
    public AudioMixerGroup audmixMixer;
    public GameObject goSound;
    public float pitchMultiplier;

    float startTickPitch;

    GameManager gm;

    void Start()
    {
        //pitchMultiplier = pitchMultiplier * 0.001f;
        tickAudio.clip = audioArr[0];
        tickAudio.Play();

        gm = GameManager.Instance;

        startTickPitch = tickAudio.pitch;
    }

    void Update()
    {
        //Debug.Log(tickAudio.pitch);
        if (gm.bomberang.timer <= 5.0f && gm.bomberang.timer > 0.0f)
        {
            tickAudio.pitch += pitchMultiplier * Time.deltaTime;
        }

        if (!(gm.bomberang.isExploded && gm.bomberang.isHeld))
        {
            if (!tickAudio.isPlaying)
            {
                tickAudio.Play();
            }
        }
        else
        {
            tickAudio.pitch = startTickPitch;
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
