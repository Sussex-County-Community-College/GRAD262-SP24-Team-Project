using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScaler : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        //Get the distance between the planet and the camera
        float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
        //Set the scale of the planet based on the distance
        transform.localScale = Vector3.one * (1f + distanceToCamera / 10f);
    }
}
