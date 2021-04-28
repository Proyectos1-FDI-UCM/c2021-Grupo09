using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour
{
    // Declaración de variables
    private int monedasTotales; // Monedas totales del jugador

    // Método que destruye el GO al que va asociado tras una colisión
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject); // La moneda desaparece
        GameManager.GetInstance().AddCoins(); ; // Se añade + 5 monedas al contador de monedas
    }
}