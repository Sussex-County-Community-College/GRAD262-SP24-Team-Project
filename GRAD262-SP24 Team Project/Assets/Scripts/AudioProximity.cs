using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AudioProximity : MonoBehaviour
{
    
        public AudioSource audioSource;
        public float proximityDistance = 5f;
        public Transform player;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (audioSource && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (audioSource && audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
    
}


