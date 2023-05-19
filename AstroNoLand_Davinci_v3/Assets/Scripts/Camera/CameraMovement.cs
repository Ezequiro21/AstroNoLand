using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Animator anim;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x= Mathf.Clamp (targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y= Mathf.Clamp (targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    public void BeginKick ()
    {
        anim.SetBool("kickactive",true);
        StartCoroutine(kickCo());

    }

    public IEnumerator kickCo()
    {
        yield return null;
        anim.SetBool("kickactive",false);

    }
    public void Begindeath ()
    {
        
        anim.SetBool("death",true);
    }

}
