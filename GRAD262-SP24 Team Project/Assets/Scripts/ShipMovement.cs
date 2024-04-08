using UnityEngine;

abstract public class ShipMovement : MonoBehaviour
{
    [Header("Thrust, Yaw, Pitch, & Roll Force")]
    public float thrustForce = 2500;
    public float maximumVelocityMagnitude = 250;
    public float yawTorque = 250;
    public float pitchTorque = -250;
    public float rollTorque = 250;

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

    [Header("Noise Scaling Exponent")]
    public float noiseScalingExponent = 3;

    [Header("Zero-velocity Easing")]
    public float easing = 0.01f;

    [Header("Type 'p' to toggle pause")]
    public bool paused = false;

    private Rigidbody _rigidBody;
    private bool _rollNoiseStart = true;
    private bool _yawNoiseStart = true;
    private bool _pitchNoiseStart = true;

    virtual protected void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    protected void SetPaused(bool value)
    {
        paused = value;
    }

    virtual protected void FixedUpdate()
    {
            ForwardThrust();
            RollMovement();
            YawMovement();
            PitchMovement();
    }

   virtual protected float GetScale()
    {
        return transform.localScale.x;
    }

    abstract protected float GetThrust();

    abstract protected float GetRoll();

    abstract protected float GetYaw();

    abstract protected float GetPitch();

    private void ForwardThrust()
    {
        Thrust(gameObject.transform.forward * (paused ? 0 : GetThrust()) * thrustForce);
    }

    private void YawMovement()
    {
        float yaw = paused ? 0 : GetYaw();

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

            Rotate(gameObject.transform.up * yawNoise * Mathf.Pow(GetScale(), noiseScalingExponent) * yawTorque);
            yawNoise = -yawNoise;
            lastYawNoise = Time.time;
        }
    }

    private void PitchMovement()
    {
        float pitch = paused ? 0 : GetPitch();

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

            Rotate(gameObject.transform.right * pitchNoise * Mathf.Pow(GetScale(), noiseScalingExponent) * pitchTorque);
            pitchNoise = -pitchNoise;
            lastPitchNoise = Time.time;
        }
    }

    private void RollMovement()
    {
        float roll = paused ? 0 : GetRoll();

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
            Rotate(gameObject.transform.forward * rollNoise * Mathf.Pow(GetScale(), noiseScalingExponent) * rollTorque);
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
}
