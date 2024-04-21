using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    public float startingDistanceFromPlayer = 5000;
    public float endingDistanceFromPlayer = 10000;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, _player.transform.position) > endingDistanceFromPlayer)
        {
            Destroy(gameObject);
        }
    }
}