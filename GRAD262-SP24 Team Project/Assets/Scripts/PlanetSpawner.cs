using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    public GameObject planetPrefab;
    public float spawnRadius = 10f;
    public int numberOfPlanets = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlanets(numberOfPlanets);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SpawnPlanets(1);
        }
    }

    void SpawnPlanets(int count)
    {
        for (int i = 0; i < numberOfPlanets; i++)
        {
            Vector3 randomPosition = Random.onUnitSphere * spawnRadius;
           Instantiate(planetPrefab, randomPosition, Quaternion.identity);
            planetPrefab.AddComponent<PlanetScaler>();
        }
    }

}
