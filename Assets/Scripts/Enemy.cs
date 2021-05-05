using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D other)
    {
        //especificamos que la colision sea con la base

        if (other.gameObject.GetComponent<Base>())
        {
            Destroy(this.gameObject);
        }
    }

}
