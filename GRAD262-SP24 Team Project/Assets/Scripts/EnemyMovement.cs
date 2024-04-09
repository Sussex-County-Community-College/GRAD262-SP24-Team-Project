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

        bool foundMovement = false;

        foreach (Movement movement in movements)
        {
            float movementStartTime = _startTime + movement.startTime;
            float movementEndTime = movementStartTime + movement.duration;

            if (Time.time >= movementStartTime && Time.time <= movementEndTime)
            {
                foundMovement = true;

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
                        transform.rotation = Quaternion.Slerp(transform.rotation, _startRotation, 0.01f);
                    }
                    else
                    {
                        _movementsThisFrame.Add(movement.type, movement);
                    }
                }
            }

            if (!foundMovement && loop)
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
