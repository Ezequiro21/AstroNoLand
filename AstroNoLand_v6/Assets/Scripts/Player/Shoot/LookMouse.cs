using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookMouse : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Camera mainCamera;
    public SpriteRenderer gunSR;



    void Start()
    {
        if (target == null)
        {
            target = transform;
        }
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPosition.z = 0;



        if (mouseWorldPosition.x < target.position.x)
        {
            gunSR.flipY = true;
        }
        else
        {
            gunSR.flipY = false;
        }

        Vector3 lookAtDirection = mouseWorldPosition - target.position;
        target.right = lookAtDirection;


    }
}
