using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ProBuilder.AutoUnwrapSettings;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;    
    public AudioClip soundClip;
    public string animationName;
    public string animationName2;

    private Animator animator;        
    private bool soundPlayed = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (audioSource == null) audioSource = GetComponent<AudioSource>(); // If not assigned, grab the AudioSource
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName(animationName) && !stateInfo.IsName(animationName2) && soundPlayed)
        {
            soundPlayed = false;
        }
        if ((stateInfo.IsName(animationName) || stateInfo.IsName(animationName2)) && !soundPlayed)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(soundClip);
            soundPlayed = true; 
        }
    }
}
