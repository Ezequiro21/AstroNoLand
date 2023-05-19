using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MeleeStrip : EnemyMovement
{


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
                }             
            }
            if (Vector3.Distance(target.position,transform.position) <= chaseRaidus && Vector3.Distance(target.position, transform.position) <= attackRadius)
            {

                StartCoroutine(AttackCo());           
            }           
       
    }
    public override void TakeDamage(float damage)
    {
        //getImpact.Play();
        Enemy enemy = GetComponent<Enemy>();
        if (enemy != null) {
            enemy.Health -= damage;
            if (enemy.Health <= 0)
            {
                //getImpact.Play();
                enemy.DeathEffect();
                enemy.MakeLoot();
                this.gameObject.SetActive(false);
            }
        }
    }
    public IEnumerator AttackCo()
    {
        //swordAttack.Play();
        currentState = EnemyState.attack;
        anim.SetBool("attack",true);
        yield return new WaitForSeconds (0.7f);
        currentState = EnemyState.walk;
        anim.SetBool("attack",false);
    }
}
