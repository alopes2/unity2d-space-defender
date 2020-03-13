using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{ 
    private WaveConfig _waveConfig;

    private List<Transform> _waypoints;

    private int _waypointIndex;

    // Start is called before the first frame update
    void Start()
    {
        _waypoints = _waveConfig.GetPathWaypoints();
        transform.position = _waypoints.ElementAt(_waypointIndex).position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        _waveConfig = waveConfig;
    }

    private void Move()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            var targetPosition = _waypoints.ElementAt(_waypointIndex).position;
            var movementThisFrame = _waveConfig.MoveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
