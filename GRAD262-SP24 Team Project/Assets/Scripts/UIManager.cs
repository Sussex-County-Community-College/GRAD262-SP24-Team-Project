using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Slider playerWeapons;

    public void PlayerShotWeapon()
    {
        playerWeapons.value--;
    }

    public int WeaponsLeft()
    {
        return (int)playerWeapons.value;
    }

    public void RefillWeapons()
    {
        playerWeapons.value = playerWeapons.maxValue;
    }
}
