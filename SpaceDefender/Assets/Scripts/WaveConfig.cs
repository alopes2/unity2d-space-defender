using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] 
    private GameObject _enemyPrefab;

    [SerializeField] 
    private GameObject _pathPrefab;

    [SerializeField] 
    private float _timeBetweenSpawns = 0.5f;

    [SerializeField] 
    private float _spawnRandomFactor = 0.3f;

    [SerializeField] 
    private int _numberOfEnemies = 5;

    [SerializeField]
    private float _moveSpeed = 2f;

    public GameObject EnemyPrefab { get => _enemyPrefab; }

    public float TimeBetweenSpawns { get => _timeBetweenSpawns; }

    public float SpawnRandomFactor { get => _spawnRandomFactor; }

    public int NumberOfEnemies { get => _numberOfEnemies; }

    public float MoveSpeed { get => _moveSpeed; }

    public List<Transform> GetPathWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform waypoint in _pathPrefab.transform)
        {
            waveWaypoints.Add(waypoint);
        }

        return waveWaypoints;
    }
}
