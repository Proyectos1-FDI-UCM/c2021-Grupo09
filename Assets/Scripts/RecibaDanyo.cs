using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecibaDanyo : MonoBehaviour
{
    int saludRestante;
    public int saludTotal = 10;

    private void Start()
    {
        saludRestante = saludTotal;
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
}
