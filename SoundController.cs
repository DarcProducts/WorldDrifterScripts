using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public List<AudioSource> audioSource;
    public List<AudioClip> audioClips;

    public bool mainMenuActivated = true;
    public bool worldAMusicActivated = false;
    public bool worldBMusicActivated = false;

    private float volume = 0f;

    public void Awake()
    {
        MainMenuMusic();
    }

    public void OnHit()
    {
        volume = Random.Range(.01f, .1f);
        audioSource[8].PlayOneShot(audioClips[8], volume);
        ResetVolume();
    }

    public void LaserShot()
    {
        volume = .5f;
        audioSource[0].PlayOneShot(audioClips[0], volume);
        ResetVolume();
    }

    public void ChargedLaserShot()
    {
        volume = .5f;
        audioSource[1].PlayOneShot(audioClips[1], volume);
        ResetVolume();
    }

    public void Explosion()
    {
        volume = Random.Range(.2f, .6f);
        audioSource[6].PlayOneShot(audioClips[6], volume);
        ResetVolume();
    }

    public void JumpSound()
    {
        volume = Random.Range(0f, .1f);
        audioSource[2].PlayOneShot(audioClips[2], volume);
        ResetVolume();
    }

    public void PlayerGrunt()
    {
        volume = Random.Range(.01f, .2f);
        audioSource[7].PlayOneShot(audioClips[7], volume);
        ResetVolume();
    }

    public void MainMenuMusic()
    {
        mainMenuActivated = true;
        volume = .02f;
        audioSource[9].PlayOneShot(audioClips[9], volume);
        ResetVolume();
    }

    public void ResetVolume()
    {
        volume = 0f;
    }
}

