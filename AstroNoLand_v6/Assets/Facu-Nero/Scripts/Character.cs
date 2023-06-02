using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    public float movSpeed,accelerationSpeed ,maxSpeed,durationJump,inicialDurationJump, oxygen, inicialOxygen;
    private Rigidbody2D CRg;
    private float axisX, axisY;
    public bool jumpMov, getOxygen;
    private Animator anim;

    GameObject inventario_prin;
    private bool inventoryVisible = false;

    // Start is called before the first frame update
    void Start()
    {
        CRg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inicialDurationJump = durationJump;
        inicialOxygen = oxygen;

        inventario_prin = GameObject.FindGameObjectWithTag("inventario_prin");
        inventario_prin.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.I))
        {
            if (!inventoryVisible)
            {
                inventoryVisible = true;
                inventario_prin.SetActive(inventoryVisible);
                GameObject.FindGameObjectWithTag("GeneralEvents").GetComponent<InventoryController>().showInventory();
            }
            else
            {
                inventoryVisible = false;
                inventario_prin.SetActive(inventoryVisible);
            }

        }



        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(axisX, axisY).normalized;
       
        anim.SetFloat("Horizontal", axisX);
        anim.SetFloat("Vertical", axisY);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LunarMoon();
        }

        //Oxigeno
        if (oxygen > 0)
        {
            oxygen -= 1 * Time.deltaTime;
        }
        if (oxygen >= inicialOxygen)
        {
            oxygen = inicialOxygen;
        }
        if (oxygen <= 0)
        {
            oxygen = 0;
        }

        if (jumpMov == true)
        {

           // Debug.Log("acelerar");
            durationJump -= Time.deltaTime;

            if (durationJump <= 0)
            {
                jumpMov = false;
            }

            movSpeed += accelerationSpeed;

            if (movSpeed >= maxSpeed)
            {
                movSpeed = maxSpeed;
            }
           
        }
        if (jumpMov == false)
        {
            //Debug.Log("desacelerar");
            jumpMov = false;
            durationJump = inicialDurationJump;
            movSpeed -= accelerationSpeed;
            if (movSpeed <= 0)
            {
                movSpeed = 0;
            }
        }
        CRg.AddForce(movement * movSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
    public void LunarMoon()
    {
        jumpMov = true;
       
    }
    private void OnTriggerStay2D(Collider2D colt)
    {
        if (colt.gameObject.tag == "Oxygen")
        {
            oxygen = oxygen + 3 * Time.deltaTime;
        }
       
    } 

/////////////////////////////////////////////////////////////////---------////////////////

    public enum CharacterState
    {
        idle,
        stagger
    }

    public CharacterState currentState;
    public Signal playerHit;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;

    public void Knock(float knockTime , float damage)
    {   
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();

        if (currentHealth.RuntimeValue <= 2)
        {
            //sonidoAntesdeMorir.Play();
        }

        if (currentHealth.RuntimeValue > 0)
        {    
            Debug.Log(currentHealth.RuntimeValue) ;
            StartCoroutine(KnockCo(knockTime));          
        }
        else
        {
            knockTime= 0;
            StartCoroutine(KnockCo(knockTime)); 
            gameObject.SetActive(false); 
             

        }
    } 
    private IEnumerator KnockCo (float knockTime)
    {
        playerHit.Raise();
        if (CRg != null)
        {      
            yield return new WaitForSeconds(knockTime);   
            CRg.velocity = Vector2.zero;
            currentState = CharacterState.idle;          
        }
    }
 ///////////////////////////////////////////////////////////////////////////////////////////////////////   
}
