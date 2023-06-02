using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public Transform point;
    public float distancia;
    public Animator anim;

    void Start()
    {
        distancia = 20f;
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Instantiate(bullet, point.position, point.rotation);
            anim.SetBool("Fire", true);
        }
        else
        {
            anim.SetBool("Fire", false);
        }

    }

    public void set_Distancia(float d)
    {
        this.distancia = d;
    }
}
