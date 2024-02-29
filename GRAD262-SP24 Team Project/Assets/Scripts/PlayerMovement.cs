using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float forwardThrustPower = 1000f;
    public float yawSpeed = 1000f;
    public float pitchSpeed = 1000f;
    public float rollSpeed = 1000f;
    public float deadZoneRadius = 0.15f;
    public bool clampRotation = true;
    public Vector3 rotationMin = new Vector3(-10, -10, -10);
    public Vector3 rotationMax = new Vector3(10, 10, 10);

    private Vector3 _centerOfScreen;
    private Rigidbody _rigidBody;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    private void FixedUpdate()
    {
        GetKeyboardInputs();
        GetMouseInputs();

        if (clampRotation)
            ClampRotation();
    }

    private float ClampAngle(float angle, float from, float to)
    {
        if (angle < 0f)
            angle = 360 + angle;

        if (angle > 180f)
            return Mathf.Max(angle, 360 + from);

        return Mathf.Min(angle, to);
    }

    private void ClampRotation()
    {
        transform.rotation = Quaternion.Euler(new Vector3(
            ClampAngle(transform.rotation.eulerAngles.x, rotationMin.x, rotationMax.x),
            ClampAngle(transform.rotation.eulerAngles.y, rotationMin.y, rotationMax.y),
            ClampAngle(transform.rotation.eulerAngles.z, rotationMin.x, rotationMax.z)));
    }

    private void GetKeyboardInputs()
    {
        ForwardThrust(Input.GetAxis("Horizontal"));
        RollMovement(Input.GetAxis("Vertical"));
    }

    private void GetMouseInputs()
    {
        Vector3 distanceFromCenter = Input.mousePosition - _centerOfScreen;

        YawMovement(distanceFromCenter.x / _centerOfScreen.x);
        PitchMovement(distanceFromCenter.y / _centerOfScreen.y);
    }

    private void ForwardThrust(float thrust)
    {
        _rigidBody.AddForce(gameObject.transform.forward * thrust * forwardThrustPower * Time.deltaTime);
    }

    private void YawMovement(float yaw)
    {
        if (Mathf.Abs(yaw) < deadZoneRadius)
            yaw = 0;

        _rigidBody.AddTorque(gameObject.transform.up * yaw * yawSpeed * Time.fixedDeltaTime);
    }

    private void PitchMovement(float pitch)
    {
        if (Mathf.Abs(pitch) < deadZoneRadius)
            pitch = 0;

        _rigidBody.AddTorque(gameObject.transform.right * pitch * pitchSpeed * Time.fixedDeltaTime);
    }

    private void RollMovement(float roll)
    {
        _rigidBody.AddTorque(gameObject.transform.forward * roll * rollSpeed * Time.fixedDeltaTime);
    }
}

