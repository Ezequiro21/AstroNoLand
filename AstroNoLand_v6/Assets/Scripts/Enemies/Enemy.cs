using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger

}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public FloatValue maxHealth;
    public float health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    public GameObject deathEffect;
    //public LootTable thisLoot;
    public EnemyMovement enemyMovement;
    public Rigidbody2D myRigidbody;
    public GameObject bullet;

    public float Health 
    {
        get { return health; }
        set { health = Mathf.Max(0, value); }
    }

    private void Start() 
    {
        enemyMovement = GetComponent<EnemyMovement>();
        //myRigidbody = enemyMovement.myRigidbody;
        myRigidbody = GetComponent<Rigidbody2D>();
        Health = maxHealth.initialValue;
    }

    public virtual void TakeDamage(float damage)
    {
        if (damage <= 0) return;
        Health -= damage;

        if (Health <= 0)
        {
            DeathEffect();
            MakeLoot();
            gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime ));
        TakeDamage(damage);
    }

    public virtual void MakeLoot()
    {
        /*if(thisLoot != null)
        {
            PowerUp current = thisLoot.LootPowerUp();
            if(current != null)
            {
                Instantiate(current.gameObject, transform.position, Quaternion.identity);
            }
        }*/
    }

    public virtual void DeathEffect()
    {
        if(deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }

    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if(myRigidbody != null)
        {   
            
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.isKinematic = true;
            currentState = EnemyState.idle;
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            
                TakeDamage(1);  
        }

      
    }*/
}
