using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Slider playerWeapons;
    public Slider asteroidHits;
    public Slider playerHealth;

    private void Start()
    {
        foreach (Laser laser in GameObject.FindObjectsOfType<Laser>())
        {
            laser.onElementLasered.AddListener(ElementLasered);
        }
    }

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

    private void ElementLasered(Laserable.LaserableElements element, float amount)
    {
        Debug.Log($"lasered element {element} amt {amount}");
    }
}
