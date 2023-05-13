using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MeleeStripShooter : MeleeStrip
{
    public GameObject bulletPrefab; // Prefab de la bala
    public float fireRate = 2f; // Velocidad de disparo en segundos
    public Transform firePoint; // el punto donde se crea la bala
    private float nextFireTime = 0f; // Tiempo del siguiente disparo
    public float bulletSpeed = 10f; // la velocidad a la que se mueve la bala
    public int bulletPoolSize = 10; // tamaño de la pool de balas
    private List<GameObject> bulletPool;

    private void Start()
    {
        currentState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        anim= GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;


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

    public override void CheckDistance()
    {
           if(Vector3.Distance(target.position, transform.position) > chaseRaidus )
            {
                anim.SetBool("wakeUp",false);
            }
            if(Vector3.Distance(target.position, transform.position) <= chaseRaidus && Vector3.Distance(target.position, transform.position) > attackRadius)
            {
                if (currentState == EnemyState.idle || currentState == EnemyState.walk && currentState != EnemyState.stagger)
                {
                  
                    Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);                
                    changeAnim(temp - transform.position);
                    myRigidbody.MovePosition(temp);
                    anim.SetBool("wakeUp",true);              
         
                    if (Time.time >= nextFireTime)
                    {
                        Shoot();
                        nextFireTime = Time.time + 1f / fireRate; // Actualiza el siguiente tiempo de disparo
                    }    
                }             
            }
    }
    public override void TakeDamage(float damage)
    {

        health -= damage;        
        if(health <= 0)
        {

            DeathEffect();
            MakeLoot();
            this.gameObject.SetActive(false);
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

        // Calcular la dirección hacia el personaje
        Vector2 direction = (target.position - firePoint.position).normalized;

        // Calcular el ángulo de rotación
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Establecer la rotación con el ángulo calculado
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = direction * bulletSpeed;
    }

}