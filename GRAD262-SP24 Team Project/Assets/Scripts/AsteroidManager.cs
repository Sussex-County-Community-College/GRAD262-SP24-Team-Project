using UnityEngine;

public class AsteroidManager : Singleton<AsteroidManager>
{
    public AsteroidField genericFieldPrefab;
    public float probabilityOfSpawningGenericField = .01f;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ExplodeAsteroid(GameObject asteroid)
    {
        Log($"enter ExplodeAsteroid: {asteroid}");
        Destroy(asteroid.gameObject);
    }

    public void SpawnField(AsteroidField prefab)
    {
        AsteroidField field = Instantiate<AsteroidField>(prefab, transform);

        field.gameObject.transform.position = _player.transform.position + _player.transform.forward * field.startingDistanceFromPlayer;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) || Random.Range(0f, 1f) < probabilityOfSpawningGenericField)
        {
            SpawnField(genericFieldPrefab);
        }
    }
}
