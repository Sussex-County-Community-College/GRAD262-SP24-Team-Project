using UnityEngine;

public class EnemyWeapons : ShipWeapons
{
    private Transform playerTransform;
    public float proximityDistance = 10f; // Adjust this to the desired proximity distance
    public float rotationSpeed = 5f; //Adjust this to the desired proximity distance
    public float cooldownTime = 2f; //Adjust this to control the cooldown time between shots
    private float lastFireTime;
    private int fireCount;
    private int maxFireCount = 5;
    private bool isFiring;
   

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastFireTime = -cooldownTime;
        fireCount = 0;
        isFiring = false;
    }
    protected override bool Fire()
    {  
        // Before firing two strategies:
        // 1. Make sure enemy is approaching the player.
        // 2. Use a Raycast forward from the enemy to the player and fire if raycast hits.
        if (!isFiring && Vector3.Distance(transform.position,playerTransform.position) < proximityDistance)
        {
            isFiring = true;
            return true;
        }
        return false;
    }

    protected override void WeaponFired()
    {
        Invoke("EnableFiring", cooldownTime);
        fireCount++;
        /*
        foreach (GameObject weapon in shipWeapons)
        {
            Vector3 targetDirection = (playerTransform.position - weapon.transform.position).normalized;

            Quaternion rotationToPlayer = Quaternion.LookRotation(targetDirection);

            GameObject missile = Instantiate(missilePrefab, weapon.transform.position,rotationToPlayer);
           
            Rigidbody missileRigidbody = missile.GetComponent<Rigidbody>();
  
            missileRigidbody.AddForce(targetDirection * missileForce * Time.fixedDeltaTime);

            

            
          
            

        }
        fireCount++;
        if (fireCount >= maxFireCount)
        {
            fireCount = 0;
            lastFireTime = Time.time;
            isFiring = false;
        }
        */
    }

    private void EnableFiring()
    {
        isFiring = false;
    }

    protected override int WeaponsLeft()
    {
        int remainingWeapons = Mathf.Max(maxFireCount - fireCount, 0);
        if (remainingWeapons == 0 && fireCount > 0)
        {
            Invoke("Reload", cooldownTime);
        }
        return remainingWeapons;
    }

    private void Reload()
    {
        fireCount = 0;
    }

    /*
     void Update()
    {
        if (Fire())
            FireWeapon();
    }

    private void FireWeapon()
    {
        Debug.Log("Enemy is firing weapon!");
        if (shipWeapons != null && shipWeapons.Length > 0)
        {
            if (WeaponsLeft() > 0)
            {
                GameObject weapon = shipWeapons[Random.Range(0, shipWeapons.Length)];
                if (weapon != null)
                {
                    GameObject missile = Instantiate(missilePrefab, weapon.transform.position, weapon.transform.rotation);
                    Rigidbody missileRigidbody = missile.GetComponent<Rigidbody>();
                    if (missileRigidbody)
                    {
                        missileRigidbody.velocity = GetComponent<Rigidbody>().velocity;

                        missileRigidbody.AddForce(transform.forward * missileForce * Time.fixedDeltaTime);
                    }
                }
                WeaponFired();
                lastFireTime = Time.time;
                isFiring = true;
            }
        }
    }
    */
}




