using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceStation : MonoBehaviour
{
    public GameObject player;

    private float _orbitingSpeed;

    private void Start()
    {
        _orbitingSpeed = GetComponent<RotateAround>().speed;
        FindObjectOfType<DockingAssist>().onDockingStateChange.AddListener(OnDockingStateChange);
    }

    private void OnDockingStateChange(DockingAssist.DockingState dockingState)
    {
        if (dockingState == DockingAssist.DockingState.dockable)
        {
            _orbitingSpeed = GetComponent<RotateAround>().speed;
            GetComponent<RotateAround>().speed = 0;
        }
        else if(dockingState == DockingAssist.DockingState.notDockable)
        {
            GetComponent<RotateAround>().speed = _orbitingSpeed;
        }
    }

}
