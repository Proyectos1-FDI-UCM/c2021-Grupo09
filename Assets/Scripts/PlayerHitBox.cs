using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    //int de la vida del player
    public int playerHP;
    //int para el daño que hace cada tipo de torre
    public int dañoBallena;
    public int dañoErizo;
    public int dañoPulpo;
    public int dañoTortuga;
    //bool para saber cuando el jugador ha muerto
    bool playerDead;
    //Valor de las monedas recogidas
    public int valorMonedas;
    //Total de monedas recogidas
    private int monedasActuales;

    //Metodo para que el GO asociado detecte una colision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //especificamos que la colision sea con cada bullet para cada tipo de torre
        if (collision.gameObject.GetComponent<BalaBallena>())
        {
            playerHP = playerHP - dañoBallena;
        }

        if (collision.gameObject.GetComponent<BalaTortuga>())
        {
            playerHP = playerHP - dañoTortuga;
        }

        if (collision.gameObject.GetComponent<BalaErizo>())
        {
            playerHP = playerHP - dañoErizo;
        }

        if (collision.gameObject.GetComponent<BalaPulpo>())
        {
            playerHP = playerHP - dañoPulpo;
        }


        //Cuando se colisione con una moneda esta se sume al total 
        if (collision.gameObject.GetComponent<Monedas>())
        {
            monedasActuales = monedasActuales + valorMonedas;
        }
    }
    //Metodo para que si el jugador se queda sin vida el GO se destruya y el bool playerDead pase a ser true
    private void Update()
    {
        if (playerHP <= 0)
        {
            Destroy(this.gameObject);

            bool playerDead = true;
        }
    }
}
