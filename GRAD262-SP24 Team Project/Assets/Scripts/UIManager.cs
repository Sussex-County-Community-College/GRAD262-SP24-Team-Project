using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Slider playerWeapons;
    public Slider asteroidHits;
    public Slider playerHealth;
    public Slider playerLaser;
    public Slider Colbolt;
    public Slider Gold;
    public Slider Platinum;
    public Slider Silver;
    public Slider Titanium;
    public float laserDebitWhenFiring = 0.1f;

    private void Start()
    {
        foreach (Laser laser in GameObject.FindObjectsOfType<Laser>())
        {
            laser.onElementLasered.AddListener(ElementLasered);
            laser.onLaserFiring.AddListener(LaserFiring);
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

        if (element == Laserable.LaserableElements.Gold)
        {
            Gold.value += amount;
        }
    }

    public void PayForResource(string element)
    {
        Debug.Log($"PayForResource element {element}");

        Laserable.LaserableElements enumElement;

        Enum.TryParse<Laserable.LaserableElements>(element, out enumElement);

        if (enumElement == Laserable.LaserableElements.Gold && Gold.value > 0)
        {
            transferValue(Gold, playerWeapons);
            transferValue(Gold, playerLaser);
            transferValue(Colbolt, playerWeapons);
            transferValue(Colbolt, playerLaser);
            transferValue(Titanium, playerWeapons);
            transferValue(Titanium, playerLaser);
            transferValue(Platinum, playerWeapons);
            transferValue(Platinum, playerLaser);
            transferValue(Silver, playerWeapons);
            transferValue(Silver, playerLaser);
        }
       
    }

    private void LaserFiring()
    {
        playerLaser.value -= laserDebitWhenFiring;
    }

    void transferValue(Slider source, Slider destination)
    {
        float transferAmt = Mathf.Min(source.value, destination.maxValue - destination.value);
        source.value -= transferAmt;
        destination.value += transferAmt;
    }

}
