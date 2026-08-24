using System.Collections.Generic;
using UnityEngine;

public class PickAndPlayRandomSound : MonoBehaviour
{
    public List<AudioClip> clips;
    public AudioSource audioSource;

    public void PlayRandom()
    {
        if (clips.Count == 0) return;

        AudioClip clip = clips[Random.Range(0, clips.Count)];
        audioSource.PlayOneShot(clip);
    }
}
