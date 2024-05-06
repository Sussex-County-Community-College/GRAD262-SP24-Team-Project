using UnityEngine;

public class EnemyWeapons : ShipWeapons
{
    public float proximityDistance = 10f; // Adjust this to the desired proximity distance
    public float cooldownTime = .5f; //Adjust this to control the cooldown time between shots
    public float reloadTime = 3f; //Adjust this to control the cooldown time between shots
    public int maxFireCount = 5;
    private int fireCount;
    private bool isFiring;

    private void Start()
    {
        fireCount = 0;
        isFiring = false;
    }

    protected override bool Fire()
    {
        if (isFiring)
            return false;

        int layerMask = LayerMask.GetMask("Player");
        RaycastHit hit;
       
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, proximityDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            return isFiring = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * proximityDistance, Color.white);
            Debug.Log("Did not Hit");
        }

        return false;
    }

    protected override void WeaponFired()
    {
        Invoke("EnableFiring", cooldownTime);
        fireCount++;
    }

    private void EnableFiring()
    {
        isFiring = false;
    }

    protected override int WeaponsLeft()
    {
        int remainingWeapons = Mathf.Max(maxFireCount - fireCount, 0);

        if (remainingWeapons == 0) // && fireCount > 0)
        {
            Invoke("Reload", cooldownTime);
        }
        return remainingWeapons;
    }

    private void Reload()
    {
        fireCount = 0;
        EnableFiring();
    }
}




