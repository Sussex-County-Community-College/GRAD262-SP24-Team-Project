using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMiningParticles : MonoBehaviour
{
      public ParticleSystem particleEffect;

        private void OnCollisionEnter(Collision collision)
        {
            // Check if the collision is happening with a specific tag, you can customize this as per your requirement
            if (collision.gameObject.CompareTag("Target"))
            {
                // Instantiate the particle effect at the position of the collision
                Instantiate(particleEffect, collision.contacts[0].point, Quaternion.identity);
            }
        }
    
}
