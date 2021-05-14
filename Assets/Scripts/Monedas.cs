using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour
{
    // Declaración de variables
    public int monedasTotales; // Monedas totales del jugador

    // Método que destruye el GO al que va asociado tras una colisión
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject); // La moneda desaparece
        //FindObjectOfType<AudioManager>().Play("Monedas"); // Reproducción del sonido de las monedas
        GameManager.GetInstance().AddCoins(); ; // Se añade + 5 monedas al contador de monedas
    }
}