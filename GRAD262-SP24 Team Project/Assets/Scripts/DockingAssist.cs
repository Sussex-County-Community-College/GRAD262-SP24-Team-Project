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
            if (!isDocked && isDockable) {
                StartDocking();
            }
            else if (isDocked) {
                StopDocking();
            }
        }
    }

    private void FixedUpdate()
    {
        if (isDocking)
        {
            ContinueDocking();
        }
    }

    private void StartDocking()
    {
        Debug.Log("DockingAssist.Update: start docking...");
        isDocking = true;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ContinueDocking()
    {
        transform.position = Vector3.MoveTowards(transform.position, dockingPort.position, approachSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, dockingPort.position) < Mathf.Epsilon)
        {
            isDocking = false;
            isDocked = true;
            onDocked.Invoke(isDocked);
        }
    }

    private void StopDocking()
    {
        Debug.Log("DockingAssist.Update: stop docking...");
        isDocked = false;
        isDocking = false;
        GetComponent<Rigidbody>().isKinematic = false;
        onDocked.Invoke(isDocked);
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
                float currentApproachSpeed = Mathf.Lerp(approachSpeed, 0f, distance / dockingDistance);
                transform.Translate(direction.normalized * currentApproachSpeed * Time.deltaTime, Space.World);
            }

        }
        else
        {
            //Docking complete
            if (Input.GetKeyDown(KeyCode.B)){
                isDocking = false;
                Debug.Log("Docking complete!");
            }
            
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
