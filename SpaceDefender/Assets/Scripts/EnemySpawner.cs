using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] 
    private List<WaveConfig> _waveConfigs;

    [SerializeField]
    private int _startingWave = 0;

    [SerializeField]
    private bool _isLooping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        } 
        while (_isLooping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = _startingWave; waveIndex < _waveConfigs.Count; waveIndex++)
        {
            var currentWave = _waveConfigs.ElementAt(waveIndex);
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig currentWave)
    {
        for (int i = 0; i < currentWave.NumberOfEnemies; i++)
        {
            var  newEnemy = Instantiate(
                currentWave.EnemyPrefab,
                currentWave.GetPathWaypoints().First().transform.position,
                Quaternion.identity);

            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(currentWave);

            yield return new WaitForSeconds(currentWave.TimeBetweenSpawns);
        }
    }
}
