public class EnemyMovement : ShipMovement
{
    override protected void Start()
    {
        base.Start();
    }

    protected override float GetPitch()
    {
        return 0;
    }

    protected override float GetRoll()
    {
        return 0;
    }

    protected override float GetThrust()
    {
        return 0;
    }

    protected override float GetYaw()
    {
        return 0;
    }
}
