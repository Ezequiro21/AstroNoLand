using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    private bool isCollected = false;
    private Collider2D playerCollider; // Variable para almacenar el collider del jugador
    private Vector3 originalScale; // Variable para almacenar la escala original

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isCollected = true;
            playerCollider = collision; // Almacena el collider del jugador
            originalScale = transform.localScale; // Almacena la escala original del objeto recolectable
        }
    }

    private void Update()
    {
        if (isCollected && Input.GetKeyDown(KeyCode.E))
        {
            if (transform.parent == playerCollider.transform)
            {
                // Obtener la posición actual del jugador
                Vector3 playerPosition = playerCollider.transform.position;

                // Dejar de ser hijo del objeto jugador
                transform.SetParent(null);

                // Establecer la posición del objeto recolectable en la posición actual del jugador
                transform.position = playerPosition;

                // Restaurar la escala original del objeto recolectable
                transform.localScale = originalScale;

                isCollected = false;
            }
            else
            {
                // Hacer que el objeto recolectable sea hijo del objeto jugador
                transform.SetParent(playerCollider.transform);

                // Ajustar la posición relativa del objeto recolectable
                transform.localPosition = new Vector3(0f, 0.5f, 0f);

                // Reducir la escala del objeto a la mitad
                transform.localScale *= 0.5f;
            }
        }
    }
}