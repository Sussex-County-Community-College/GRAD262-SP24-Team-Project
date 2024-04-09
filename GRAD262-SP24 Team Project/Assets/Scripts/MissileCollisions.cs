using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Missle hit Asteroid!");
            UIManager.Instance.AsteroidBlast();
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Missle hit Enemy!");
        }
    }

    
}
