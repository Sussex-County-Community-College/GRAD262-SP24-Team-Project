using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingAssist : MonoBehaviour
{
    public Transform dockingPort;
    public float alignmentSpeed = 2f;
    public float approachSpeed = 5f;
    public float dockingDistance = 1f;
    public LayerMask obstacleMask;

    private bool isDocking = false;

    private void Update()
    {
        if (isDocking)
        {
            AlignWithDockingPort();
            ApproachDockingPort();
            CheckPlayerMovement();
        }
    }

    public void StartDocking()
    {
        isDocking = true;
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
            if(distance > dockingDistance)
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
