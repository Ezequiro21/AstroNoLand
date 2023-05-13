using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MeleeStrip
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;


    public override void CheckDistance()
    {
        
            if(Vector3.Distance(target.position, transform.position) > chaseRaidus )
            {
                anim.SetBool("wakeUp",true);  
                if(Vector3.Distance(transform.position, path[currentPoint].position) > float.Epsilon) 
                {
                    Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);         
                    changeAnim(temp - transform.position);
                    myRigidbody.MovePosition(temp);                
                }
                else
                {
                    ChangeGoal();
                }
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
                    //audioSource.PlayOneShot((Sword), 0.5f);
                    StartCoroutine(AttackCo());           
            }
             
    }

    private void ChangeGoal()
    {
        if(currentPoint == path.Length - 1)
        {
            currentPoint = 0;
        }
        else
        {
            currentPoint ++;
        }
        currentGoal = path[currentPoint];
    }
}