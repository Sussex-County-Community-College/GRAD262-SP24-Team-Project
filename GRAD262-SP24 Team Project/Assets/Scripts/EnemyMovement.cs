using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : ShipMovement
{
    public enum MovementType { idle, thrust, roll, yaw, pitch, restore, die };

    [System.Serializable]
    public struct Movement
    {
        public MovementType type;
        public float value;
        public float startTime;
        public float duration;
    }

    [Header("Patrol Movement")]
    public Movement[] movements;

    private float _startTime = 0;
    private Quaternion _startRotation;
    private Dictionary<MovementType, Movement> _movementsThisFrame = new Dictionary<MovementType, Movement>();

    override protected void Start()
    {
        base.Start();
        _startTime = Time.time;
        _startRotation = transform.rotation;
    }

    protected void Update()
    {
        _movementsThisFrame.Clear();

        foreach (Movement movement in movements)
        {
            Debug.Log($"type={ movement.type.ToString()}");

            float movementStartTime = _startTime + movement.startTime;
            float movementEndTime = movementStartTime + movement.duration;

            if (Time.time > movementStartTime && Time.time < movementEndTime)
            {
                Debug.Log($"active type={ movement.type.ToString()}");

                if (_movementsThisFrame.ContainsKey(movement.type))
                    Debug.LogWarning($"ignoring key {movement.type} overlap, startTime={movement.startTime}");
                else
                {
                    if (movement.type == MovementType.die)
                    {
                        Destroy(gameObject);
                    }
                    else if (movement.type == MovementType.restore)
                    {
                        GetComponent<Rigidbody>().isKinematic = true;
                        transform.rotation = Quaternion.Slerp(transform.rotation, _startRotation, 0.5f);
                    }
                    else
                    {
                        GetComponent<Rigidbody>().isKinematic = false;
                        _movementsThisFrame.Add(movement.type, movement);
                    }
                }
            }

            if (_movementsThisFrame.Count == 0)
                _startTime = Time.time;
        }

        
    }

    protected override float GetPitch()
    {
        if (_movementsThisFrame.ContainsKey(MovementType.pitch))
            return _movementsThisFrame[MovementType.pitch].value;
        return 0;
    }

    protected override float GetRoll()
    {
        if (_movementsThisFrame.ContainsKey(MovementType.roll))
            return _movementsThisFrame[MovementType.roll].value;
        return 0;
    }

    protected override float GetThrust()
    {
        if (_movementsThisFrame.ContainsKey(MovementType.thrust))
            return _movementsThisFrame[MovementType.thrust].value;
        return 0;
    }

    protected override float GetYaw()
    {
        if (_movementsThisFrame.ContainsKey(MovementType.yaw))
            return _movementsThisFrame[MovementType.yaw].value;
        return 0;
    }

}
