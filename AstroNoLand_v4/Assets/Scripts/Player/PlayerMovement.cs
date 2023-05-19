using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    shooting,
    stagger 
}
public class PlayerMovement : Player
{
    public float moveSpeed = 5f; // la velocidad de movimiento del personaje
    public float dashSpeed = 10f; // la velocidad del dash del personaje
    public float dashDuration = 0.5f; // la duración del dash en segundos
    private float moveInputHorizontal;
    private float moveInputVertical;
    private Animator anim;
    private bool isDashing = false; // indica si el personaje está haciendo un dash actualmente
    private float dashTimer = 0f; // un temporizador para realizar el dash durante un período de tiempo limitado
    public PlayerState currentState;
    public GameObject knife;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentState = PlayerState.idle; 
        knife.SetActive(false);
        
    }

    public void Update()
    {
        move();
    }

    public void move()
    {
        moveInputHorizontal = Input.GetAxisRaw("Horizontal");
        moveInputVertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.C) && !isDashing)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }
        if(Input.GetKeyDown(KeyCode.X)  && currentState != PlayerState.attack && currentState != PlayerState.stagger && currentState != PlayerState.shooting )
        {
     
            StartCoroutine(AttackCo()); 
        }
    

    
        Debug.Log(dashSpeed);
        Vector2 movement = new Vector2(moveInputHorizontal, moveInputVertical).normalized;
        if (isDashing)
        {
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer > 0f)
            {
                myRigidbody.velocity = movement * dashSpeed;
            }
            else
            {
                isDashing = false;
                myRigidbody.velocity = movement * moveSpeed;
            }
        }
        else
        {
            myRigidbody.velocity = movement * moveSpeed;
        }

        Debug.Log("Velocidad: " + myRigidbody.velocity.magnitude.ToString("0.00"));
        FlipSprite();
        if (movement.magnitude > 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

    }
    public IEnumerator AttackCo()
    {

        knife.SetActive(true);
        currentState = PlayerState.attack;
        yield return null;
        yield return new WaitForSeconds(.33f);
        knife.SetActive(false);
        currentState = PlayerState.walk;
    }

    public void FlipSprite()
    {
        if (moveInputHorizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (moveInputHorizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

}