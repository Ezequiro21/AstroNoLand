using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Oxigen : PowerUp
{
    public FloatValue timeToAdd; // Cantidad de tiempo que se agregará a la barra de vida
    private PlayerLifeBar playerLifeBar; // Referencia al script PlayerLifeBar
    public GameObject playerLifeBarObject; // Referencia al objeto del jugador
    public GameObject countDownLifeObject; // Referencia al objeto del jugador
    public CountDownLife countDownLife;

    private void Start()
    {
        // Obtener una referencia a PlayerLifeBar en el objeto del jugador
        playerLifeBar = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLifeBar>();
        // Obtener una referencia al Slider de la barra de vida del jugador
        playerLifeBar.lifeBarSlider = GameObject.FindGameObjectWithTag("LifeBarSlider").GetComponent<Slider>();
        countDownLifeObject = GameObject.FindGameObjectWithTag("CountDownLife");
        countDownLife = countDownLifeObject.GetComponent<CountDownLife>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {

            playerLifeBar.AddLifeTime(timeToAdd); // Agregar tiempo a la barra de vida del jugador
            countDownLife.AddLifeTime(timeToAdd);
            powerupSignal.Raise();           
            Destroy(this.gameObject);

        }
    }
}


