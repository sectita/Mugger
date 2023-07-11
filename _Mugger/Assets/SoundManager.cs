using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip punch, jump, swardHurt, punchkickVoice;
    static AudioSource _audioSource;

    public AudioSource LoopAudioSource;
    public AudioClip LoopClip;

    // Start is called before the first frame update
    void Start()
    {

        _audioSource = GetComponent<AudioSource>();
        LoopAudioSource = GetComponent<AudioSource>();


        punch              = Resources.Load<AudioClip>("_punch");
                             
        jump               = Resources.Load<AudioClip>("_jump");
                             
        swardHurt          = Resources.Load<AudioClip>("_swardHurt");
                             
        punchkickVoice     = Resources.Load<AudioClip>("_punchkickVoice");



        LoopAudioSource.PlayOneShot(LoopClip);
        LoopAudioSource.PlayScheduled(AudioSettings.dspTime + LoopClip.length);

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "_punch":
                _audioSource.PlayOneShot(punch);
                break;
            case "_jump":
                _audioSource.PlayOneShot(jump);
                break;
            case "_swardHurt":
                _audioSource.PlayOneShot(swardHurt);
                break;
            case "_punchkickVoice":
                _audioSource.PlayOneShot(punchkickVoice);
                break;

        }
    }
}
