using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audios;
    public AudioSource bg_audio_play;
    public AudioSource effect_audio_play;
    void Start()
    {
        bg_audio_play = transform.GetChild(0).GetComponent<AudioSource>();
        effect_audio_play = transform.GetChild(1).GetComponent<AudioSource>();

        bg_audio_play.volume = 0.5f;
    }
    void Update()
    {
        
    }


    public void sound_effect(int num)
    {
        if(num == 2)
        {
            effect_audio_play.volume = 0.2f;
        }
        else
        {
            effect_audio_play.volume = 1;
        }
        effect_audio_play.PlayOneShot(audios[num]);
    }
}
