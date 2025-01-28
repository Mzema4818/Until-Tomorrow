using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTree : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip choppingTree;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChoppingTree()
    {
        audioSource.PlayOneShot(choppingTree);
    }

    public void BreakingTree()
    {
        LumberWorker lumberWorker = transform.GetComponent<LumberWorker>();
        Destroy(lumberWorker.Tree.transform.parent.gameObject);
    }
}
