using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMiningParticles : MonoBehaviour
{
      public ParticleSystem particleEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            // Assuming you have a ParticleSystem component attached to the GameObject
            GetComponent<ParticleSystem>().Play();
        }
    }


}
