using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotZone : MonoBehaviour
{
    public float slingshotStrength = 10f; //Strength of the slingshot effect
    public float range = 10f; //Range of the slingshot zone

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 direction = transform.position - rb.position;
            float distance = direction.magnitude;
            if(distance <= range)
            {
                Vector3 velocityBoost = direction.normalized * slingshotStrength;
                rb.velocity += velocityBoost;
            }
        }
    }
}
