using UnityEngine;

abstract public class ShipWeapons : MonoBehaviour
{
    public GameObject missilePrefab;
    public float missileForce = 100000f;
    public GameObject[] shipWeapons;

    abstract protected bool Fire();

    abstract protected void WeaponFired();

    abstract protected int WeaponsLeft();

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
            }
        }
        
    }
}
