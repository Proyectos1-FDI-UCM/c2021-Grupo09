using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour
{
    private float momentoSpawn;
    public float tiempoDespawn = 10f;
    private void Start()
    {
        momentoSpawn = Time.time;
    }

    private void Update()
    {
        if(Time.time >= momentoSpawn + tiempoDespawn)
        {
            Destroy(this.gameObject);
        }
    }

    // Método que destruye el GO al que va asociado tras una colisión
    private void OnTriggerEnter2D(Collider2D other)
    {
        AudioManager.GetInstance().Play("Monedas"); // Reproducción del sonido de las monedas
        GameManager.GetInstance().AddCoins(); // Se añade + 5 monedas al contador de monedas
        Destroy(this.gameObject); // La moneda desaparece
    }
}