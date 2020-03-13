using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private int _health = 100;
    [SerializeField] private int _scoreValue = 150;

    [Header("Shooting")]
    private float _shotCounter;
    [SerializeField] private float _minTimeBetweenShots = 0.2f;
    [SerializeField] private float _maxTimeBetweenShots = 3f;
    [SerializeField] private GameObject _projectile;
    [SerializeField] private float _projectileSpeed = 10f;

    [Header("Effects")]
    [SerializeField] private GameObject _deathVfx;
    [SerializeField] private float _explosionDuration = 0.2f;
    [SerializeField] private AudioClip _deathSfx;
    [SerializeField] [Range(0,1)] private float _deathSfxVolume = 0.2f;
    [SerializeField] private AudioClip _laserSfx;
    [SerializeField] [Range(0, 1)] private float _laserSfxVolume = 0.2f;

    private GameSession _gameSession;
    // Start is called before the first frame update
    void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
        _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var damageDealer = collider.gameObject.GetComponent<DamageDealer>();

        if (!damageDealer)
        {
            return;
        }

        ProcessHit(damageDealer);
    }

    private void CountDownAndShoot()
    {
        _shotCounter -= Time.deltaTime;
        if (_shotCounter <= 0f)
        {
            Shoot();
            _shotCounter = Random.Range(_minTimeBetweenShots, _maxTimeBetweenShots);
        }
    }

    private void Shoot()
    {
        var laser = Instantiate(
            _projectile,
            transform.position,
            Quaternion.identity);
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -_projectileSpeed);
        AudioSource.PlayClipAtPoint(_laserSfx, Camera.main.transform.position, _laserSfxVolume);
    }

    public void ProcessHit(DamageDealer damageDealer)
    {
        _health = damageDealer.Hit(_health);

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _gameSession.AddToScore(_scoreValue);
        Destroy(gameObject);
        var explosion = Instantiate(_deathVfx, transform.position, transform.rotation);
        AudioSource.PlayClipAtPoint(_deathSfx, Camera.main.transform.position, _deathSfxVolume);
        Destroy(explosion, _explosionDuration);
    }
}
