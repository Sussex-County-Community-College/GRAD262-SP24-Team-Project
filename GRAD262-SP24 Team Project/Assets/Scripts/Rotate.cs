using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Vector3 rotationForce = new Vector3(0, 0, 1000000);
    public float angularVelocityMin = 1;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (_rb.angularVelocity.magnitude < angularVelocityMin)
            _rb.AddTorque(rotationForce * Time.fixedDeltaTime);
    }
}
