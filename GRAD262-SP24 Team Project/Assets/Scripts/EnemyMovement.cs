using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 1;
    public float minPursueDistance = 1000;
    public float minAttackDistance = 250;
    public GameObject player;

    private int? _currentWaypoint = null;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWorldObjects waypointSpawner = GetComponent<SpawnWorldObjects>();
        List<Transform> waypointList = new List<Transform>();

        for (int i = 0; i < waypointSpawner.transform.childCount; i++)
        {
            Transform waypointTransform =  waypointSpawner.transform.GetChild(i);

            if (waypointTransform.CompareTag("Waypoint"))
            {
                waypointList.Add(waypointTransform);
            }
        }

        waypoints = waypointList.ToArray();

        foreach(Transform waypointTransform in waypoints)
        {
            waypointTransform.SetParent(transform.parent);
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceFromPlayer < minAttackDistance)
        {
            Attack();
        }
        else if (distanceFromPlayer < minPursueDistance)
        {
            Pursue();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (_currentWaypoint == null)
        {
            _currentWaypoint = FindClosestWaypoint();
        }
        else if (Vector3.Distance(transform.position, waypoints[(int)_currentWaypoint].transform.position) < Mathf.Epsilon)
        {
            _currentWaypoint = (_currentWaypoint + 1) % waypoints.Length;
        }

        transform.LookAt(waypoints[(int)_currentWaypoint].transform);
        transform.position = Vector3.MoveTowards(transform.position, waypoints[(int)_currentWaypoint].transform.position, speed * Time.deltaTime);
    }

    private void Pursue()
    {
        Debug.Log($"enemy {name} pursuing...");
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        _currentWaypoint = null;
    }

    private void Attack()
    {
        Debug.Log($"enemy {name} attacking...");
        _currentWaypoint = null;
    }

    private int FindClosestWaypoint()
    {
        float minDistance = float.MaxValue;
        int closest = -1;

        for (int i = 0; i < waypoints.Length; i++)
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, waypoints[i].transform.position);

            if (distanceToWaypoint < minDistance)
            {
                minDistance = distanceToWaypoint;
                closest = i;
            }
        }

        return closest;
    }
}
