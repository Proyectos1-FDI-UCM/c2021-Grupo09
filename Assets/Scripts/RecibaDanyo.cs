using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecibaDanyo : MonoBehaviour
{
    // Declaración de variables
    int saludRestante; // Salud que posee actualmente el enemigo
    public int saludTotal = 10; // Salud máxima y con la que empieza el enemigo
    int monedasSalud; // Monedas que soltará dependiendo de la salud del enemigo
    public GameObject moneda; // Prefab de moneda que aparecerá al morir el enemigo

    private void Start()
    {
        monedasSalud = saludTotal / 5; // Cálculo de monedas según la salud del enemigo
        saludRestante = saludTotal;
    }

    private void Update()
    {
        if (saludRestante <= 0) // Si la salud del enemigo se reduce a cero
        {
            Destroy(this.gameObject); // Éste se "destruye"
            GameManager.GetInstance().enemigosTotales--;
            Debug.Log("Ahora mismo hay " + GameManager.GetInstance().enemigosTotales + " enemigos en el nivel");
            if (gameObject.GetComponent<DivideEnDos>() != null)
            {
                gameObject.GetComponent<DivideEnDos>().Divide();
            }
            else if (gameObject.GetComponent<SpawnArea>() != null)
            {
                gameObject.GetComponent<SpawnArea>().Spawn();
            }
            for (int i = 0; i < monedasSalud; i++) // Y aparecerán tantas monedas
            {
                Instantiate(moneda, transform.position, transform.rotation); // Como sean necesarias
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Bala>() != null)
        {
            saludRestante -= other.gameObject.GetComponent<Bala>().damageDealt;
            // Borrar al hacer que pueda morir
            Debug.Log(saludRestante + " HP restante");
        }
    }
    public void CurarEnemigo(int cura)
    {
        if (saludRestante < saludTotal)
        {
            saludRestante += cura;
            if (saludRestante > saludTotal)
            {
                saludRestante = saludTotal;
            }
        }
    }

    public void DanarEnemigo(int dano)
    {
        saludRestante -= dano;
    }

}
