using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if( hit != null)
            {
                hit.isKinematic = false ; 
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                if(other.gameObject.CompareTag("enemy") /*&& other.isTrigger*/)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;
                    other.GetComponent<Enemy>().Knock(hit,knockTime, damage);
                }
                if(other.gameObject.CompareTag("Player"))
                {
                    hit = other.GetComponent<Rigidbody2D>();
                    if (hit != null)
                    {
                        hit.isKinematic = false;
                        difference = hit.transform.position - transform.position;
                        difference = difference.normalized * thrust;
                        hit.AddForce(difference, ForceMode2D.Impulse);
                        Character Character = other.GetComponent<Character>();
                        if (Character.currentState != Character.CharacterState.stagger)
                        {
                            Character.currentState = Character.CharacterState.stagger;
                            Character.Knock(knockTime, damage);
                        }
                    }
                }
                /*if (other.gameObject.CompareTag("Player"))
                {
                    difference = hit.transform.position - transform.position;
                    difference = difference.normalized * thrust;
                    hit.AddForce(difference, ForceMode2D.Impulse);
                    Character character = other.GetComponent<Character>();
                    if (character.currentState != Character.CharacterState.stagger)
                    {
                        character.currentState = Character.CharacterState.stagger;
                        character.Knock(knockTime, damage);
                    }
                } */         
            }
        }
    }    
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                hit.isKinematic = false;
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                hit.AddForce(difference, ForceMode2D.Impulse);
                
                if (other.gameObject.CompareTag("enemy"))
                {
                    Enemy enemy = other.GetComponent<Enemy>();
                    enemy.currentState = EnemyState.stagger;
                    enemy.Knock(hit, knockTime, damage);
                }
                else if (other.gameObject.CompareTag("Player"))
                {
                    Character character = other.GetComponent<Character>();
                    if (character.currentState != Character.CharacterState.stagger)
                    {
                        character.currentState = Character.CharacterState.stagger;
                        character.Knock(knockTime, damage);
                    }
                }
            }
        }
    }    
}*/

