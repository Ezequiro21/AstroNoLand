using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { Up, Down, Left, Right }
public class Turret : Enemy
{
    private const float MIN_FIRE_RATE = 0.1f; // Límite mínimo de velocidad de disparo
    private const float MAX_FIRE_RATE = 10f; // Límite máximo de velocidad de disparo
    private const float MIN_BULLET_SPEED = 1f; // Límite mínimo de velocidad de bala
    private const float MAX_BULLET_SPEED = 100f; // Límite máximo de velocidad de bala

    public Direction fireDirection = Direction.Down; // Dirección del disparo

    private float _fireRate = 2f; // Velocidad de disparo en segundos
    public float FireRate
    {
        get { return _fireRate; }
        set { _fireRate = Mathf.Clamp(value, MIN_FIRE_RATE, MAX_FIRE_RATE); }
    }

    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint; // el punto donde se crea la bala
    [SerializeField] private float nextFireTime = 0f; // Tiempo del siguiente disparo
    private float _bulletSpeed = 10f; // la velocidad a la que se mueve la bala
    public float BulletSpeed
    {
        get { return _bulletSpeed; }
        set { _bulletSpeed = Mathf.Clamp(value, MIN_BULLET_SPEED, MAX_BULLET_SPEED); }
    }

    public int bulletPoolSize = 10; // tamaño de la pool de balas
    private List<GameObject> bulletPool;
    public Transform target;
    public float chaseRadius;

    private void Start()
    {
        firePoint.GetComponent<SpriteRenderer>().enabled = false;
        // Inicializar la pool de balas
        bulletPool = new List<GameObject>();
        for (int i = 0; i < bulletPoolSize; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    private void Update()
    {
        if(Vector3.Distance(target.position, transform.position) < chaseRadius )
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / FireRate; // Actualiza el siguiente tiempo de disparo
            }
        }    
    }

    private void Shoot()
    {
        // Buscar una bala inactiva en la pool
        GameObject bullet = bulletPool.Find(b => !b.activeSelf);

        if (bullet == null)
        {
            // Si no hay balas inactivas, crear una nueva instancia
            bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }

        bullet.SetActive(true);
        bullet.transform.position = firePoint.position;

        
        Vector2 direction = Vector2.zero;
        switch (fireDirection)
        {
            case Direction.Up:
                direction = Vector2.up;
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case Direction.Down:
                direction = Vector2.down;
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, -90f);
                break;
            case Direction.Left:
                direction = Vector2.left;
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
                break;
            case Direction.Right:
                direction = Vector2.right;
                bullet.transform.rotation = Quaternion.Euler(0f, 0f, -180f);
                break;
        }

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * BulletSpeed;
    }
}