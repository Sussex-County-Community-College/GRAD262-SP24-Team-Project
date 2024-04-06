using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceStation : MonoBehaviour
{
    public GameObject player;
    public float maxDockingDistance = 500f;
    public UnityEvent<SpaceStation, bool> onPlayerDockable;

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
            onPlayerDockable.Invoke(this, true);
        }
        else
        {
            GetComponent<RotateAround>().speed = _orbitingSpeed;
            onPlayerDockable.Invoke(this, false);
        }
    }
}
