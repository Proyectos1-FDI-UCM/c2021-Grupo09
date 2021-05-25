using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public int vidaBase;
    public int dañoRecibido;

    private GameManager instance;

    private void Start()
    {
        instance = GameManager.GetInstance();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //especificamos que la colision sea con un enemigo
        if (collision.gameObject.GetComponent<RecibaDanyo>() != null)
        {
            instance.HurtBase(dañoRecibido);
            instance.enemigosTotales--;
            AudioManager.GetInstance().Play("DañoBase");
            Destroy(collision.gameObject);
        }
    }
}
