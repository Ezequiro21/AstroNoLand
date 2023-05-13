using UnityEngine;
using TMPro;

public class CountDownLife : MonoBehaviour
{
    public float startingValue = 10f; // Valor inicial del contador
    public float countdownSpeed = 1f; // Velocidad a la que disminuye el contador
    public TextMeshProUGUI counterText; // Referencia al objeto TextMeshProUGUI de la interfaz donde se muestra el contador

    private float currentValue;

    void Start()
    {
        currentValue = startingValue;
    }

    void Update()
    {
        // Disminuir el contador a una velocidad determinada
        currentValue -= countdownSpeed * Time.deltaTime;

        // Actualizar el texto de la interfaz
        counterText.text = FormatTime(currentValue);

        // Si el contador llega a cero o menos, detenerlo
        if (currentValue <= 0f)
        {
            currentValue = 0f;
            countdownSpeed = 0f;
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void AddLifeTime(FloatValue timeToAdd)
{
    currentValue += timeToAdd.RuntimeValue; // Sumar tiempo al contador

    if (currentValue > startingValue) // Verificar si el tiempo actual excede el valor inicial
    {
        currentValue = startingValue; // Establecer el tiempo actual al valor inicial
    }

    counterText.text = FormatTime(currentValue); // Actualizar el texto de la interfaz
}
}