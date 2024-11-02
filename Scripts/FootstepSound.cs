using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    [Header("Walk Sound References")]
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private FirstPersonController playerControllerReference;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        PlayWalkSound();
    }

    private void PlayWalkSound()
    {

       if (playerControllerReference.isGrounded && playerControllerReference.rb.velocity.magnitude > 2f && !audioSource.isPlaying)
        {
            audioSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.Play();
        }
    }
}
