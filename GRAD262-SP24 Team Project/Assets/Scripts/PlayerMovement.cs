using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private DockingPort _dockingPort;

    [Header("Thrust, Yaw, Pitch, & Roll Force")]
    public float thrustForce = 10000f;
    public float maximumVelocityMagnitude = 1000f;
    public float yawTorque = 1000f;
    public float pitchTorque = -1000f;
    public float rollTorque = 1000f;

    [Header("Roll Noise")]
    public float rollNoise = 1f;
    public float rollNoiseInterval = 2.5f;
    public float lastRollNoise = 0;

    [Header("Yaw Noise")]
    public float yawNoise = 1f;
    public float yawNoiseInterval = 5f;
    public float lastYawNoise = 0;

    [Header("Pitch Noise")]
    public float pitchNoise = 1f;
    public float pitchNoiseInterval = 3f;
    public float lastPitchNoise = 0;

    [Header("Mouse Deadzone; Zero-velocity Easing")]
    public float deadZoneRadius = 0.5f;
    public float easing = 0.01f;

    [Header("Type 'p' to toggle pause")]
    public bool paused = false;

    private Vector3 _centerOfScreen;
    private Rigidbody _rigidBody;
    private bool _rollNoiseStart = true;
    private bool _yawNoiseStart = true;
    private bool _pitchNoiseStart = true;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _centerOfScreen = new Vector3(Screen.width / 2, Screen.height / 2);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            paused = !paused;
        if(_dockingPort != null)
        {
            _dockingPort.DockShip();
        }
    }

    private void FixedUpdate()
    {
        if (!paused)
        {
            GetKeyboardInputs();
            GetMouseInputs();
        }
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
        Thrust(gameObject.transform.forward * thrust * thrustForce);
    }

    private void YawMovement(float yaw)
    {
        if (Mathf.Abs(yaw) < deadZoneRadius)
            yaw = 0;

        if (yaw != 0)
        {
            Rotate(gameObject.transform.up * yaw * yawTorque);
            lastYawNoise = Time.time;
        }
        else if (Time.time > lastYawNoise + yawNoiseInterval)
        {
            if (_yawNoiseStart)
            {
                _yawNoiseStart = false;

                if (Random.Range(0, 1) < .5)
                    yawNoise = -yawNoise;
            }

            Rotate(gameObject.transform.up * yawNoise * yawTorque);
            yawNoise = -yawNoise;
            lastYawNoise = Time.time;
        }
    }

    private void PitchMovement(float pitch)
    {
        if (Mathf.Abs(pitch) < deadZoneRadius)
            pitch = 0;

        if (pitch != 0)
        {
            Rotate(gameObject.transform.right * pitch * pitchTorque);
            lastPitchNoise = Time.time;
        }
        else if (Time.time > lastPitchNoise + pitchNoiseInterval)
        {
            if (_pitchNoiseStart)
            {
                _pitchNoiseStart = false;

                if (Random.Range(0, 1) < .5)
                    pitchNoise = -pitchNoise;
            }

            Rotate(gameObject.transform.right * pitchNoise * pitchTorque);
            pitchNoise = -pitchNoise;
            lastPitchNoise = Time.time;
        }
    }

    private void RollMovement(float roll)
    {
        if (roll != 0)
        {
            Rotate(gameObject.transform.forward * roll * rollTorque);
            lastRollNoise = Time.time;
        }
        else if (Time.time > lastRollNoise + rollNoiseInterval)
        {
            if (_rollNoiseStart)
            {
                _rollNoiseStart = false;

                if (Random.Range(0, 1) < .5)
                    rollNoise = -rollNoise;
            }
            Rotate(gameObject.transform.forward * rollNoise * rollTorque);
            rollNoise = -rollNoise;
            lastRollNoise = Time.time;
        }
    }

    private void Thrust(Vector3 vector)
    {
        if (!Mathf.Approximately(0f, vector.magnitude))
        {
            if (_rigidBody.velocity.magnitude < maximumVelocityMagnitude)
                _rigidBody.AddForce(vector * Time.fixedDeltaTime);
        }
        else
            _rigidBody.velocity = Vector3.Lerp(_rigidBody.velocity, Vector3.zero, easing);
    }

    private void Rotate(Vector3 vector)
    {
        if (!Mathf.Approximately(0f, vector.magnitude))
            _rigidBody.AddTorque(vector * Time.fixedDeltaTime);
    }

    public void SetDockingPort(DockingPort dockingPort)
    {
        _dockingPort = dockingPort;
    }

    public void Docked()
    {
        //Handle ship being docked
        //Disable ship movement, etc/
    }
}

