using UnityEngine;

public class PlayerWeapons : ShipWeapons
{
    protected override bool Fire()
    {
        return Input.GetKeyDown(KeyCode.F);
    }

    protected override void WeaponFired()
    {
        UIManager.Instance.PlayerShotWeapon();
    }

    protected override int WeaponsLeft()
    {
        return UIManager.Instance.WeaponsLeft();
    }
}
