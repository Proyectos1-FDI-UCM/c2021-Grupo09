using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SombraBalaBallena : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BalaBallena>()) Destroy(gameObject);
    }
}
