using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //especificamos que la colision sea con la base

        if (collision.gameObject.GetComponen<Base>())
        {
            Destroy(this.gameObject);
        }
    }

}
