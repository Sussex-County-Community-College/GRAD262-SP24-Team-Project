using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileCollisions : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if (CompareTag("Asteroid"))
        {
            Debug.Log("Missle hit");
            UIManager.Instance.AsteroidBlast();
        }
    }

    
}
