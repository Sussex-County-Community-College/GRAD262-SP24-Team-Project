using UnityEngine;

public class EnemyWeapons : ShipWeapons
{
   
    protected override bool Fire()
    {
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




