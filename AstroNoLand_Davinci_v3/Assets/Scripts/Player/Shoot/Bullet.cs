using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    Vector3 position;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.velocity = transform.right * speed;
        position = transform.position;
        Destroy(gameObject, 4);

    }

    // Update is called once per frame
    void Update()
    {
       if(position != transform.position)
        {
            transform.right = transform.position - position;
            position = transform.position;
        }
    }
}
