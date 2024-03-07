
using UnityEngine;


public class PlayerWeapons : MonoBehaviour
{
    public GameObject missilePrefab;
    public float missileForce = 1000f;
    public GameObject[] shipWeapons;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            FireWeapon();
    }

    private void FireWeapon()
    {
        if (UIManager.Instance.WeaponsLeft() > 0)
        {
            GameObject weapon = shipWeapons[Random.Range(0, shipWeapons.Length)];
            GameObject missile = Instantiate(missilePrefab, weapon.transform.position, weapon.transform.rotation);
            Rigidbody rb = missile.GetComponent<Rigidbody>();

            if (rb)
            {
                rb.AddForce(transform.forward * missileForce  * Time.fixedDeltaTime);
            }

            UIManager.Instance.PlayerShotWeapon();
        }
        else
        {
            UIManager.Instance.RefillWeapons();
        }
    }
}
