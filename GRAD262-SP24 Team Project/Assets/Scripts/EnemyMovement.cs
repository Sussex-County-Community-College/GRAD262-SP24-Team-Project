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
    public bool loop = false;

    private float _startTime = 0;
    private float _endTime = 0;
    private Dictionary<MovementType, Movement> _movementsThisFrame = new Dictionary<MovementType, Movement>();

    override protected void Start()
    {
        base.Start();
        SetMovementStartAndEndTimes();
    }

    private void SetMovementStartAndEndTimes()
    {
        _startTime = Time.time;

        foreach (Movement movement in movements)
        {
            _endTime = Mathf.Max(_endTime, _startTime + movement.startTime + movement.duration);
        }
    }

    protected void Update()
    {
        _movementsThisFrame.Clear();

        foreach (Movement movement in movements)
        {
            float movementStartTime = _startTime + movement.startTime;
            float movementEndTime = movementStartTime + movement.duration;

            if (Time.time > movementStartTime && Time.time < movementEndTime)
            {
                if (_movementsThisFrame.ContainsKey(movement.type))
                    Debug.LogWarning($"ignoring key {movement.type} overlap, startTime={movement.startTime}");
                else
                {
                    switch (movement.type)
                    {
                        case MovementType.die:
                            Destroy(gameObject);
                            break;
                        case MovementType.restore:
                            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.identity, 0.01f);
                            break;
                        case MovementType.thrust:
                        case MovementType.pitch:
                        case MovementType.roll:
                        case MovementType.yaw:
                            _movementsThisFrame.Add(movement.type, movement);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (Time.time > _endTime && loop)
            {
                SetMovementStartAndEndTimes();
            }
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
