using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothing;
    public Animator anim;

    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothing);
    }

    public void BeginKick()
    {
        anim.SetBool("kickactive", true);
        StartCoroutine(KickCo());
    }

    private IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("kickactive", false);
    }
}