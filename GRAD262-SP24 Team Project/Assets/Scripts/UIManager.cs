using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Slider playerWeapons;
    public Slider asteroidHits;
    public Slider playerHealth;

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
    public void AsteroidBlast()
    {
        asteroidHits.value++;
    }

    public void ShipHealth()
    {
        playerHealth.value--;
    }
}
