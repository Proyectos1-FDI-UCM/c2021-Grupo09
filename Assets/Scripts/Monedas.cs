using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour
{
    //Metodo que destruye el GO al que va asociado tras una colisión
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(this.gameObject);

    }
}