using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bullet;
    public float distancia;

    void Start()
    {
        distancia = 20f;
    }


    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }

    }

    public void set_Distancia(float d)
    {
        this.distancia = d;
    }
}
