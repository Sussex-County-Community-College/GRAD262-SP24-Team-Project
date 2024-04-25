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

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        lastFireTime = -cooldownTime;
        fireCount = 0;
    }
    protected override bool Fire()
    { if (Vector3.Distance(transform.position,playerTransform.position) < proximityDistance)
        {
            Debug.Log("Enemy is in range to fire!");
            //Rotate the enemy towards the player
            Vector3 targetDirection = playerTransform.position - transform.position;
            float step= rotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);

            //Check if enough time has passed since the last shot
            if (Time.time - lastFireTime >= cooldownTime && fireCount < maxFireCount)
            {
                Debug.Log("Firing missile");
                //Fire a missile
                WeaponFired();
                lastFireTime = Time.time; //Update the last fire time
                fireCount++;
            }
            
            return true;
        }
        return false;
    }

    protected override void WeaponFired()
    {
        foreach (GameObject weapon in shipWeapons)
        {
            GameObject missile = Instantiate(missilePrefab, weapon.transform.position, weapon.transform.rotation);
            Rigidbody missileRigidbody = missile.GetComponent<Rigidbody>();
            missileRigidbody.AddForce(weapon.transform.forward * missileForce * Time.fixedDeltaTime); 
        }
    }

    protected override int WeaponsLeft()
    {
        return int.MaxValue;
    }
}




