using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Configuration parameters
    [Header("Player")]
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _health = 200;

    [Header("Projectile")]
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _projectileFirePeriod = 0.1f;
    
    [Header("Sound Effects")]
    [SerializeField] private AudioClip _deathSfx;
    [SerializeField] [Range(0, 1)] private float _deathSfxVolume = 0.2f;
    [SerializeField] private AudioClip _laserSfx;
    [SerializeField] [Range(0, 1)] private float _laserSfxVolume = 0.2f;

    private Coroutine _firingCoroutine;

    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    public int Health { get => _health; }

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
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
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(_deathSfx, Camera.main.transform.position, _deathSfxVolume);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //InvokeRepeating(nameof(FireContinuously), 0f, _projectileFirePeriod); One solution for repeating actions. Needs to cancel invoke below
            
            //Assigning Coroutine here but it is not needed, as we don't need to cancel it anymore
            _firingCoroutine = StartCoroutine(FireContinuously());
        }

        // Don't need this part as I'm checking for Input.GetButton in FireContinuously method
        // This is just an example for stopping Coroutiines
        //if (Input.GetButtonUp("Fire1"))
        //{
            //CancelInvoke(nameof(FireContinuously)); One solution for repeating actions
            //StopCoroutine(_firingCoroutine); 
        //}
    }

    //void FireContinuously()
    //{
    //        var laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
    //        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _projectileSpeed);
    //}

    IEnumerator FireContinuously()
    {
        while (Input.GetButton("Fire1"))
        {
            var laser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, _projectileSpeed);
            AudioSource.PlayClipAtPoint(_laserSfx, Camera.main.transform.position, _laserSfxVolume);
            yield return new WaitForSeconds(_projectileFirePeriod);
        }
    }

    private void SetUpMoveBoundaries()
    {
        var mainCamera = Camera.main;
        var maxPoint = new Vector3(1,1,0);
        var minPoint = new Vector3(0,0,0); 

        var spriteRenderer = GetComponent<SpriteRenderer>();
        var spriteHalfSize = spriteRenderer.bounds.extents;

        xMin = mainCamera.ViewportToWorldPoint(minPoint).x + spriteHalfSize.x;
        xMax = mainCamera.ViewportToWorldPoint(maxPoint).x - spriteHalfSize.x;

        yMin = mainCamera.ViewportToWorldPoint(minPoint).y + spriteHalfSize.y;
        yMax = mainCamera.ViewportToWorldPoint(maxPoint).y - spriteHalfSize.y;
    }

    private void Move()
    {
        // This is for manually calculating boundaries
        // var spriteRenderer = GetComponent<SpriteRenderer>();
        // var spriteHalfSize = spriteRenderer.bounds.extents;
        // transform.position = new Vector2(GetXPosition(spriteHalfSize.x), GetYPosition(spriteHalfSize.y));

        transform.position = new Vector2(GetXPosition(), GetYPosition());
    }

    // private float GetXPosition(float spriteHalfWidth)This is for manually calculating boundaries
    private float GetXPosition()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;

        var newXPosition = transform.position.x + deltaX;

        // Manually calculating boundaries
        //var cameraWidth = GetCameraWidthInUnits();

        //var xMin = (spriteHalfWidth) - GetCameraHorizontalOffset();
        //var xMax = (cameraWidth - spriteHalfWidth) - GetCameraHorizontalOffset();

        return Mathf.Clamp(newXPosition, xMin, xMax);
    }

    //private float GetYPosition(float spriteHalfHeight) This is for manually calculating boundaries
    private float GetYPosition()
    {
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;

        var newYPosition = transform.position.y + deltaY;

        // Manually calculating boundaries
        //var cameraHeight = GetCameraHeightInUnits();

        //var yMin = (spriteHalfHeight) - GetCameraVerticalOffset();
        //var yMax = (cameraHeight - spriteHalfHeight) - GetCameraVerticalOffset();

        return Mathf.Clamp(newYPosition, yMin, yMax);
    }

    private float GetCameraWidthInUnits()
    {
        return GetCameraHeightInUnits() * Camera.main.aspect;
    }

    private float GetCameraHeightInUnits()
    {
        return 2f * Camera.main.orthographicSize;
    }

    private float GetCameraHorizontalOffset()
    {
        return (GetCameraWidthInUnits() / 2 - Camera.main.transform.position.x ) ;
    }

    private float GetCameraVerticalOffset()
    {
        return (GetCameraHeightInUnits() / 2 - Camera.main.transform.position.y);
    }
}
