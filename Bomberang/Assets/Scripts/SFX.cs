using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SFX : MonoBehaviour
{

    public AudioClip throwClip;
    public AudioClip explosionClip;
    //public GameObject bomberang;
    public AudioMixerGroup audmixMixer;
    public GameObject goSound;

    public void PlayThrowAudio()
    {
        // Instantiates the sound GameObject.
        GameObject sound = Instantiate(goSound);
        AudioSource sfxSource = sound.GetComponent<AudioSource>();

        // The sound effect source's mixer group is now the same as the m_audMixMixer.
        sfxSource.outputAudioMixerGroup = audmixMixer;
        // Sets the source's clip to a randomized index in the throw array.
        sfxSource.clip = throwClip;
        // Play the current clip.
        sfxSource.Play();
        // This varies the pitch randomly to emulate different clips.
        sfxSource.pitch = Random.Range(0.9f, 1.1f);

        Destroy(sound, 3.0f);
    }

    public void PlayExplodeAudio()
    {
        // Instantiates the sound GameObject.
        GameObject sound = Instantiate(goSound);
        AudioSource sfxSource = sound.GetComponent<AudioSource>();

        // The sound effect source's mixer group is now the same as the m_audMixMixer.
        sfxSource.outputAudioMixerGroup = audmixMixer;
        // Sets the source's clip to a randomized index in the throw array.
        sfxSource.clip = explosionClip;
        // Play the current clip.
        sfxSource.Play();
        // This varies the pitch randomly to emulate different clips.
        sfxSource.pitch = sfxSource.pitch + Random.Range(-0.1f, 0.1f);

        Destroy(sound, 3.0f);
    }
}
