using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrust = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        KeyboardMovement();
    }

    void KeyboardMovement()
    {
        float h = Input.GetAxis("Horizontal");

        GetComponent<Rigidbody>().AddForce(transform.forward * h * thrust * Time.deltaTime);
    }
}
