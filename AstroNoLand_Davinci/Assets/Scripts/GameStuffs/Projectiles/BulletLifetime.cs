using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BulletLifetime : MonoBehaviour
{
    public float lifetime = 2f;

    private float timer;

    private void OnEnable()
    {
        timer = lifetime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // En lugar de destruir la bala, la desactivamos y la devolvemos al pool
            gameObject.SetActive(false);
        }
    }
}  



