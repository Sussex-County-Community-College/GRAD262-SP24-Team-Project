using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spawns spawnCount (e.g., 40) objects within the Random.Range(-spawnRange, spawnRange)
///   and sets their localScale to Vector3.one * Random.Range(scaleMin, scaleMax).
/// You can also add a random Y rotation if you want, so they aren't all oriented the
///   same way.
/// Note that this is attached to the same GameObject multiple times!
/// </summary>
public class SpawnWorldObjects : MonoBehaviour
{
    [Header("Inscribed")]
    [Tooltip("The number of these objects to spawn")]
    public int spawnCount = 40;
    [Tooltip("The maximum x, y, & z position for spawned GameObjects")]
    public Vector3 spawnRangeMax = new Vector3(512, 0, 512);
    [Tooltip("The minimum x, y, & z position for spawned GameObjects")]
    public Vector3 spawnRangeMin = new Vector3(-512, 0, -512);
    [Tooltip("The maximum uniform localScale for spawned GameObjects")]
    public float scaleMax = 80;
    [Tooltip("The minimum uniform localScale for spawned GameObjects")]
    public float scaleMin = 20;
    [Tooltip("The List of prefabs that can be spawned by this script")]
    public List<GameObject> prefabs;

   
    void Awake()
    {
        // for i from =0 to <spawnCount
        for (int i = 0; i < spawnCount; i++)
        {
            // Instantiate a random GameObject gObj from the prefabs List
            GameObject gObj = Instantiate(prefabs[Random.Range(0, prefabs.Count)], transform);

            // Set x, y, and z values of a Vector3 pos from spawnRange values
            Vector3 pos = new Vector3(
                Random.Range(spawnRangeMin.x, spawnRangeMax.x),
                Random.Range(spawnRangeMin.y, spawnRangeMax.y),
                Random.Range(spawnRangeMin.z, spawnRangeMax.z)
                );

            // Set the position of gObj to pos
            gObj.transform.localPosition = pos;

            // Set the localScale of gObj to be betweeb scaleMin and scaleMax
            gObj.transform.localScale = Vector3.one * Random.Range(scaleMin, scaleMax);
        }
    }

}
