using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public EnemyGroup[] groups;
    public float probabilityOfSpawningGroup = .01f;

    private GameObject _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void SpawnGroup(EnemyGroup prefab)
    {
        EnemyGroup group = Instantiate<EnemyGroup>(prefab, transform);

        group.gameObject.transform.position = _player.transform.position + _player.transform.forward * group.startingDistanceFromPlayer;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) || Random.Range(0f, 1f) < probabilityOfSpawningGroup)
        {
            SpawnGroup(groups[Random.Range(0, groups.Length)]);
        }
    }
}
