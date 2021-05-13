using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideEnDos : MonoBehaviour
{
    public GameObject enemigoSpawneadoPrefab;

    public void Divide()
    {
        GameManager.GetInstance().enemigosTotales += 2;
        Debug.Log("Ahora mismo hay " + GameManager.GetInstance().enemigosTotales + " enemigos en el nivel");
        Instantiate(enemigoSpawneadoPrefab, new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y + 0.5f), Quaternion.identity);
        Instantiate(enemigoSpawneadoPrefab, new Vector2 (gameObject.transform.position.x - 0.5f, gameObject.transform.position.y - 0.5f), Quaternion.identity);
    }
}
