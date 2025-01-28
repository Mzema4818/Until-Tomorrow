using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footsteps : MonoBehaviour
{
    public AudioClip[] clips;
    public GameObject Dust;
    private AudioSource audioSource;
    public Transform dustParent;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Step(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.2)
        {
            AudioClip clip = GetRandomClip();
            audioSource.PlayOneShot(clip, 0.01f);
        }

        GameObject dust = Instantiate(Dust, transform.position, Quaternion.identity, dustParent);
        Destroy(dust, 1f);
    }

    private AudioClip GetRandomClip()
    {
        return clips[Random.Range(0, clips.Length)];
    }
}