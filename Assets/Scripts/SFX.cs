using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField] AudioClip[] starSoundClip;
    public float volume;


    public void PlaySound(int x)
    {
        AudioSource.PlayClipAtPoint(starSoundClip[x], transform.position, volume);
    }
}