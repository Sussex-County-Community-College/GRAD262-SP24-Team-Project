using UnityEngine;

public class EnemyMovement : ShipMovement
{
    public enum MovementType { idle, thrust, roll, yaw, pitch, die };

    [System.Serializable]
    public struct Movement
    {
        public MovementType type;
        public float value;
        public float duration;
    }

    [Header("Patrol Movement")]
    public Movement[] movements;

    public int currentMovement = 0;
    public float timeOfNextMovement = 0;

    protected void Update()
    {
        if (Time.time > timeOfNextMovement)
        {
            currentMovement = (currentMovement + 1) % movements.Length;
            timeOfNextMovement = Time.time + movements[currentMovement].duration;

            if (movements[currentMovement].type == MovementType.die)
                Destroy(gameObject);
        }
    }

    override protected void Start()
    {
        base.Start();
        
        if (movements.Length > 0)
        {
            currentMovement = 0;
            timeOfNextMovement = Time.time + movements[currentMovement].duration;
        }
    }

    protected override float GetPitch()
    {
        if (movements.Length > 0 && movements[currentMovement].type == MovementType.pitch)
            return movements[currentMovement].value;
        return 0;
    }

    protected override float GetRoll()
    {
        if (movements.Length > 0 && movements[currentMovement].type == MovementType.roll)
            return movements[currentMovement].value;
        return 0;
    }

    protected override float GetThrust()
    {
        if (movements.Length > 0 && movements[currentMovement].type == MovementType.thrust)
            return movements[currentMovement].value;
        return 0;
    }

    protected override float GetYaw()
    {
        if (movements.Length > 0 && movements[currentMovement].type == MovementType.yaw)
            return movements[currentMovement].value;
        return 0;
    }
}
