using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingPort : MonoBehaviour
{
    public Transform dockedShipPosition;
    public float dockingSpeed = 1.0f;
    public float dockingRotationSpeed = 1.0f;

    private Transform _dockedShip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _dockedShip = other.transform;
            //_dockedShip.GetComponent<PlayerMovement>().SetDockingPort(this);
        }
    }

    public void DockShip()
    {
        if (_dockedShip != null)
        {
            //Align the ship with the docking port
            _dockedShip.rotation = Quaternion.Slerp(_dockedShip.rotation, transform.rotation, dockingRotationSpeed * Time.deltaTime);

            //Check if the ship has docked
            float distancetoDock = Vector3.Distance(_dockedShip.position, dockedShipPosition.position);
            if (distancetoDock < 0.1f)
            {
                //Ship is docked
                //_dockedShip.GetComponent<PlayerMovement>().Docked();
                _dockedShip = null;
            }
        }
    }
}
