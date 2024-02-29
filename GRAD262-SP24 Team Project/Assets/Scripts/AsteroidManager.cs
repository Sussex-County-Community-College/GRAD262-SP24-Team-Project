using UnityEngine;

public class AsteroidManager : Singleton<AsteroidManager>
{
    public void ExplodeAsteroid(GameObject asteroid)
    {
        Log($"enter ExplodeAsteroid {asteroid.name}");
        Destroy(asteroid.gameObject);
    }
}
