using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDown : MonoBehaviour
{
    public Transform playerShip;
    public float minScale = 0.1f;
    public float scaleSpeed = 0.1f;

    // Update is called once per frame
   private void Update()
    {
        float distanceToSingularity = Vector3.Distance(transform.position, playerShip.position);
        float scale = Mathf.Lerp(1f, minScale, distanceToSingularity * scaleSpeed);
        playerShip.localScale = new Vector3(scale, scale, scale);
    }
}
