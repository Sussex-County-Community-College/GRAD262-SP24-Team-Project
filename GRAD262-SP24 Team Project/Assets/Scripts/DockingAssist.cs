using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DockingAssist : MonoBehaviour
{
    public enum DockingState { notDockable, dockable, docking, docked, undocking };
    public DockingState dockingState = DockingState.notDockable;
    public Transform dockingPort;
    public float approachSpeed = 5f;
    public float approachRotationEasing = 0.01f;
    public UnityEvent<DockingState> onDockingStateChange;

    private void Start()
    {
        dockingPort = GameObject.FindGameObjectWithTag("DockingPort").transform;
    }
    private void Update()
    {
        if (dockingState == DockingState.dockable)
        {
            StartDocking();
        }
        else if (dockingState == DockingState.docked && Input.GetKeyDown(KeyCode.B))
        {
            StopDocking();
        }
    }

    private void FixedUpdate()
    {
        if (dockingState == DockingState.docking)
        {
            ContinueDocking();
        }
    }

    private void StartDocking()
    {
        dockingState = DockingState.docking;
        onDockingStateChange.Invoke(dockingState);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ContinueDocking()
    {
        transform.position = Vector3.MoveTowards(transform.position, dockingPort.position, approachSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, dockingPort.parent.rotation, approachRotationEasing);

        bool rotationComplete = 1 - Mathf.Abs(Quaternion.Dot(transform.rotation, dockingPort.parent.rotation)) < Mathf.Epsilon;

        if (Vector3.Distance(transform.position, dockingPort.position) < Mathf.Epsilon && rotationComplete)
        {
            dockingState = DockingState.docked;
            onDockingStateChange.Invoke(dockingState);
            GetComponent<Rigidbody>().isKinematic = false;
            GetComponent<PlayerMovement>().paused = true;
        }
    }

    private void StopDocking()
    {
        GetComponent<PlayerMovement>().paused = false;
        dockingState = DockingState.undocking;
        onDockingStateChange.Invoke(dockingState);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (dockingState == DockingState.notDockable && other.gameObject.CompareTag("DockingPort"))
        {
            dockingState = DockingState.dockable;
            onDockingStateChange.Invoke(dockingState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("DockingPort"))
        {
            if (dockingState == DockingState.undocking)
            {
                dockingState = DockingState.notDockable;
                onDockingStateChange.Invoke(dockingState);
            }
            else if (dockingState == DockingState.dockable)
            {
                dockingState = DockingState.notDockable;
                onDockingStateChange.Invoke(dockingState);
            }
        }
    }

}
