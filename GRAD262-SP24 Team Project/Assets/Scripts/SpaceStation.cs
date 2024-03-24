using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    public GameObject player;
    public float maxDockingDistance = 1000;

    private float _orbitingSpeed;

    private void Start()
    {
        _orbitingSpeed = GetComponent<RotateAround>().speed;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < maxDockingDistance)
        {
            _orbitingSpeed = GetComponent<RotateAround>().speed;
            GetComponent<RotateAround>().speed = 0;
        }
        else
        {
            GetComponent<RotateAround>().speed = _orbitingSpeed;
        }
    }
}
