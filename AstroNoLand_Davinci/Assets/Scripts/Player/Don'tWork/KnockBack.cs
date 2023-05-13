using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust = 1;
    public float knockbackTime = 0.1f;
    public float damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player playerHealth = collision.gameObject.GetComponent<Player>();
            if (playerHealth != null)
            {
                playerHealth.Knock(knockbackTime, damage);
                Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRigidbody != null)
                {
                    Vector2 direction = (playerRigidbody.transform.position - transform.position).normalized;
                    playerRigidbody.AddForce(direction * thrust, ForceMode2D.Impulse);
                    StartCoroutine(KnockCo(playerRigidbody, knockbackTime));
                }
            }
        }
        else if (collision.gameObject.CompareTag("enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
                Rigidbody2D enemyRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                enemy.Knock( knockbackTime, damage);
                if (enemyRigidbody != null)
                {
                    Vector2 direction = (enemyRigidbody.transform.position - transform.position).normalized;
                    enemyRigidbody.AddForce(direction * thrust, ForceMode2D.Impulse);
                    StartCoroutine(KnockCo(enemyRigidbody, knockbackTime));
                }
            }
        }
    }

    public IEnumerator KnockCo(Rigidbody2D rigidbody, float knockTime)
    {
        if (rigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            rigidbody.velocity = Vector2.zero;
            rigidbody.isKinematic = true;
        }
    }
}