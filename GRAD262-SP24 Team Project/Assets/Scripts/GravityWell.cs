using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    public float gravitationalConstant = 6.67430e-11f; // Gravitational constant (adjust as needed)
    public float mass = 1.0f; // Mass of the gravity well (adjust as needed)

    private void FixedUpdate()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 2f); //Check for objects within the gravity well
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = transform.position - collider.transform.position;
                float distance = direction.magnitude;
                if (distance > 0.1f) //Avoid division by zero
                {
                    float forceMagnitude = gravitationalConstant * (mass * rb.mass) / (distance * distance);
                    Vector3 force = direction.normalized * forceMagnitude;
                    rb.AddForce(force, ForceMode.Force);
                }
            }
        }
    }
}
