using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Player
{
    public float movSpeed,accelerationSpeed ,maxSpeed,durationJump,inicialDurationJump;
    private Rigidbody2D CRg;
    private float axisX, axisY;
    public bool jumpMov;

    private Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        CRg = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inicialDurationJump = durationJump;
    }

    // Update is called once per frame
    void Update()
    {
        axisX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(axisX, axisY).normalized;
       
        anim.SetFloat("Horizontal", axisX);
        anim.SetFloat("Vertical", axisY);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            LunarMoon();
        }

        
        if (jumpMov == true)
        {

            Debug.Log("acelerar");
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
            Debug.Log("desacelerar");
            jumpMov = false;
            durationJump = inicialDurationJump;
            movSpeed -= accelerationSpeed;
            if (movSpeed <= 0)
            {
                movSpeed = 0;
            }
        }
        CRg.AddForce(movement * movSpeed, ForceMode2D.Impulse);
        
    
    }

    
    public void LunarMoon()
    {
        jumpMov = true;
       
    }
}
/*Vector2 movement = new Vector2(axisX, axisY).normalized;
float inicialDuartionJump = durationJump;
CRg.AddForce(new Vector2(movement, mov), ForceMode2D.Force);
// CRg.velocity = movement * mov;
// CRg.velocity = movement * dashSpeed;
if (jumpMov == true)
{
    Debug.Log("hola");
    //  CRg.velocity = movement * mov;
    durationJump = -1 * Time.deltaTime;
    if (durationJump <= 0)
    {
        //CRg.velocity = movement * mov * -1 * Time.deltaTime ;

        jumpMov = false;
        durationJump = inicialDuartionJump;
    }
}
if (jumpMov == false)
{
    CRg.velocity = movement * 0;

}
*/
