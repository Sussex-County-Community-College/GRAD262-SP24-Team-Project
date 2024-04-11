using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DockingAssist : MonoBehaviour
{
    public Transform dockingPort;
    public float alignmentSpeed = 2f;
    public float approachSpeed = 5f;
    public float dockingDistance = 100f;
    public LayerMask obstacleMask;

    public bool isDocking = false;

    public SpaceStation dockableSpaceStation;
    public bool isDockable = false;

    public bool isDocked = false;

    public UnityEvent<bool> onDocked;

    private void Start()
    {
        foreach (SpaceStation station in GameObject.FindObjectsOfType<SpaceStation>())
        {
            station.onPlayerDockable.AddListener(OnPlayerDockable);
        }
    }

    private void OnPlayerDockable(SpaceStation station, bool dockable)
    {
        isDockable = dockable;
        dockableSpaceStation = station;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isDocked)
            {
                isDocked = false;
                onDocked.Invoke(isDocked);
            }
            else if (isDockable)
            {
                isDocked = true;
                onDocked.Invoke(isDocked);
                
            }
            else
                Debug.Log("not dockable");
        }
    }

    private void AlignWithDockingPort()
    {
        Vector3 targetDirection = dockingPort.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, alignmentSpeed * Time.deltaTime);
    }

    private void ApproachDockingPort()
    {
        Vector3 direction = dockingPort.position - transform.position;
        float distance = direction.magnitude;
        if (distance > dockingDistance)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit, distance, obstacleMask))
            {
                Vector3 avoidDirection = Vector3.Reflect(direction.normalized, hit.normal);
                transform.Translate(direction.normalized * approachSpeed * Time.deltaTime, Space.World);
            }
            else
            {
                transform.Translate(direction.normalized * approachSpeed * Time.deltaTime, Space.World);
            }

        }
        else
        {
            //Docking complete
            isDocking = false;
            Debug.Log("Docking complete!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Set the player's rigidbody iskematic property to true
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if(playerRigidbody != null)
            {
                playerRigidbody.isKinematic = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Set the player's rigidbody iskematic property to false
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if(playerRigidbody != null)
            {
                playerRigidbody.isKinematic = false;
            }
        }
    }
    private void CheckPlayerMovement()
    {
        float moveInput = Input.GetAxis("Vertical"); //Assuming vertical axis controls forward/backward movement
        if (moveInput != 0f)
        {
            isDocking = false;
            Debug.Log("Docking canceled due to player movement.");
        }
    }
}
