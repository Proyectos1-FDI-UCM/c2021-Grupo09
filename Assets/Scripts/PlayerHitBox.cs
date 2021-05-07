using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    //Valor de las monedas recogidas
    public int valorMonedas;
    //Total de monedas recogidas
    private int monedasActuales;

    //Metodo para que el GO asociado detecte una colision
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //Cuando se colisione con una moneda esta se sume al total 
        if (collision.gameObject.GetComponent<Monedas>())
        {
            monedasActuales = monedasActuales + valorMonedas;
        }
    }
    
}
