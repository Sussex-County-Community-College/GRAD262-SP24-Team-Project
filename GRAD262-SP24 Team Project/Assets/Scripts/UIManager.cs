using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Slider playerWeapons;
    public Slider asteroidHits;
    public Slider playerHealth;
    public Slider playerLaser;
    public Slider cobalt;
    public Slider gold;
    public Slider platinum;
    public Slider silver;
    public Slider titanium;
    public float laserDebitWhenFiring = 0.1f;

    private Dictionary<Laserable.LaserableElements, Slider> _elementSliders = new Dictionary<Laserable.LaserableElements, Slider>();

    private void Start()
    {
        foreach (Laser laser in GameObject.FindObjectsOfType<Laser>())
        {
            laser.onElementLasered.AddListener(ElementLasered);
            laser.onLaserFiring.AddListener(LaserFiring);
        }

        _elementSliders.Add(Laserable.LaserableElements.Cobalt, cobalt);
        _elementSliders.Add(Laserable.LaserableElements.Gold, gold);
        _elementSliders.Add(Laserable.LaserableElements.Platinum, platinum);
        _elementSliders.Add(Laserable.LaserableElements.Silver, silver);
        _elementSliders.Add(Laserable.LaserableElements.Titanium, titanium);

        FindObjectOfType<DockingAssist>().onDockingStateChange.AddListener(OnDockingStateChange);
    }

    private void OnDockingStateChange(DockingAssist.DockingState dockingState)
    {
        if (dockingState == DockingAssist.DockingState.docked)
        {
            foreach (Slider source in _elementSliders.Values)
            {
                transferValue(source, playerWeapons);
            }

            foreach (Slider source in _elementSliders.Values)
            {
                transferValue(source, playerLaser);
            }
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

        if (element != Laserable.LaserableElements.None)
            _elementSliders[element].value += amount;
    }

    private void LaserFiring()
    {
        playerLaser.value -= laserDebitWhenFiring;
    }

    private void transferValue(Slider source, Slider destination)
    {
        float transferAmt = Mathf.Min(source.value, destination.maxValue - destination.value);

        source.value -= transferAmt;
        destination.value += transferAmt;
    }

}
