using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            Debug.Log("Missle hit");
            UIManager.Instance.AsteroidBlast();
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }

    
}
