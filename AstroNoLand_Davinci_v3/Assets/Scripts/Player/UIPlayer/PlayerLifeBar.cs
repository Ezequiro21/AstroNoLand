using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLifeBar : MonoBehaviour
{
    private bool isDead = false; // si el jugador está muerto
    public Slider lifeBarSlider; // barra de vida del jugador
    public FloatValue currentLifeTime;

    private void Start()
    {
        currentLifeTime.RuntimeValue = currentLifeTime.initialValue;
        lifeBarSlider.maxValue = currentLifeTime.initialValue;
        lifeBarSlider.value = currentLifeTime.RuntimeValue;
    }

    private void Update()
    {
        if (!isDead)
        {
            currentLifeTime.RuntimeValue -= Time.deltaTime;

            if (currentLifeTime.RuntimeValue <= 0)
            {
                currentLifeTime.RuntimeValue = 0;
                isDead = true;
                Debug.Log("Player is dead!");
                Destroy(gameObject);
            }

            lifeBarSlider.value = currentLifeTime.RuntimeValue;
        }
    }

    public void AddLifeTime(FloatValue timeToAdd)
    {
        currentLifeTime.RuntimeValue += timeToAdd.RuntimeValue; // Sumar tiempo a la barra de vida

        if (currentLifeTime.RuntimeValue > currentLifeTime.initialValue) // Verificar si el tiempo actual de vida excede el tiempo máximo de vida
        {
            currentLifeTime.RuntimeValue = currentLifeTime.initialValue; // Establecer el tiempo actual de vida al máximo tiempo de vida
        }

        lifeBarSlider.value = currentLifeTime.RuntimeValue; // Actualizar el valor de la barra de vida
    }
}

 




