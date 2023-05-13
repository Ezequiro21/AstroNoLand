using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // el prefab de la bala
    public Transform firePoint; // el punto donde se crea la bala
    public float bulletSpeed = 10f; // la velocidad a la que se mueve la bala
    public int bulletPoolSize = 10; // tamaño de la pool de balas
    private List<GameObject> bulletPool;
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerMovement.currentState = PlayerState.shooting;
            Fire();
            
            firePoint.GetComponent<SpriteRenderer>().enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerMovement.currentState = PlayerState.walk;
            firePoint.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    private void Fire()
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
       bullet.transform.rotation = firePoint.rotation;

      Vector2 bulletDirection = firePoint.right;
      if (transform.localScale.x < 0)
      {
        bulletDirection = -bulletDirection;
      }

      Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
      bulletDirection.Normalize();
      Vector2 playerDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
      bulletRb.velocity = (bulletDirection * bulletSpeed) + (playerDirection * bulletSpeed);

      // agregar el script BulletLifetime a la bala
      bullet.AddComponent<BulletLifetime>();
    }
}