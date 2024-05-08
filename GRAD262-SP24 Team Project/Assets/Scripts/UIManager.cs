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
    private bool _isPaused = false;
    private PlayerMovement _playerMovement;

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

        _playerMovement = FindObjectOfType<PlayerMovement>();
        _isPaused = _playerMovement.paused;

        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        playerHealth.onTakeDamage.AddListener(OnPlayerHealthChange);
    }

    private void OnPlayerHealthChange(float health)
    {
        if (health < 3) // 3/10 health threshold
        {
            FlashLowHealthSign();
        }

        playerHealth.value = health;
    }

    private void FlashLowHealthSign()
    {
        Log("FlashLowHealthSign");
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) // Change "P" to any other key if needed
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        _isPaused = !_isPaused;
        _playerMovement.paused = _isPaused;

        if (_isPaused)
        {
            //Time.timeScale = 0; // Set the time scale to 0 to pause the game
            Debug.Log("Game Paused, press P to unpasue");
        }
        else
        {
            //Time.timeScale = 1; // Set the time scale back to 1 to resume the game
            Debug.Log("Game Resumed");
        }
    }
    public class Player
    {
        private float health;

        public Player(float initialHealth)
        {
            health = initialHealth;
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if (health < 0)
            {
                health = 0;
            }

            if (health < 0.25f) // 25% health threshold
            {
                FlashLowHealthSign();
            }
        }

        private void FlashLowHealthSign()
        {
            Console.WriteLine("LOW HEALTH!");
            // Implement your logic for flashing the low health sign here
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player(100); // Initialize player with 100 health

            // Simulate taking damage
            player.TakeDamage(30); // Health drops to 70
            player.TakeDamage(20); // Health drops to 50
            player.TakeDamage(30); // Health drops to 20, low health sign should flash

            Console.ReadLine(); // Keep console window open
        }
    }
}