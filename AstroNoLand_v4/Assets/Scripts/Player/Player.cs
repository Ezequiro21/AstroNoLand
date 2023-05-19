using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float lifeTimeForLiFetimeBar = 120f; // tiempo de vida en segundos reflejados en la barra de la interefaz
    public CameraMovement cameraMovement;
    public Rigidbody2D myRigidbody;
    public Signal playerHit;
    public FloatValue currentHealth;
    private float _currentHealth;
    public Signal playerHealthSignal;
    public float initialHealth ;

    public float CurrentHealth 
    {
        get { return _currentHealth; }
        set 
        { 
            _currentHealth = value;
            playerHealthSignal.Raise();
        }
    }

    private void Start()
    {
        StartCoroutine(DestroyAfterTime());
        myRigidbody = GetComponent<Rigidbody2D>();
        currentHealth.RuntimeValue = initialHealth;
    }
    public void Knock(float knockTime , float damage)
    {   
        
        Debug.Log(currentHealth.RuntimeValue);
        if (currentHealth.RuntimeValue >= 0)
        {     
            currentHealth.RuntimeValue -= damage;
            playerHealthSignal.Raise();
            StartCoroutine(KnockCo(myRigidbody, knockTime));          
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    } 
    private IEnumerator KnockCo (Rigidbody2D myRigidbody, float knockTime)
    {
        playerHit.Raise();
        if (myRigidbody != null)
        {      
            yield return new WaitForSeconds(knockTime);   
            myRigidbody.velocity = Vector2.zero;   
        }
    }
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(lifeTimeForLiFetimeBar);
        Destroy(gameObject);
    }
}
/*public void TakeDamageFromKnockback(float damage)
{   
    currentHealth.RuntimeValue -= damage;
    playerHealthSignal.Raise();

    if (currentHealth.RuntimeValue <= 0)
    {     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     
    }
}*/