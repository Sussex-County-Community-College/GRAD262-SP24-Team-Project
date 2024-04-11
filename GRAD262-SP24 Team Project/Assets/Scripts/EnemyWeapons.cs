using UnityEngine;

public class EnemyWeapons : ShipWeapons
{
    protected override bool Fire()
    {
        return false;
    }

    protected override void WeaponFired()
    {
    }

    protected override int WeaponsLeft()
    {
        return int.MaxValue;
    }
}




