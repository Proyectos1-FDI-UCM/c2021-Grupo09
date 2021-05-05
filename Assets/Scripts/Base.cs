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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //especificamos que la colision sea con un enemigo
        if (collision.gameObject.GetComponent<Enemy>() != null)
        {
            instance.HurtBase(dañoRecibido);
            Destroy(collision.gameObject);
        }
    }
}
