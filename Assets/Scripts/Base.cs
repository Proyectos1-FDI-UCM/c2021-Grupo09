using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int vidaBase;
    public int dañoRecibido;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //especificamos que la colision sea con un enemigo
        if (collision.gameObject.GetComponent<Enemy>())
        {
            vidaBase -= dañoRecibido;
        }
    }
}
