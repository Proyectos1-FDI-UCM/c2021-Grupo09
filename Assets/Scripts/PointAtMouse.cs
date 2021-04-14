using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Posición de ratón (En la escena, no en la pantalla)
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Dirección hacia el ratón es su posición menos la del jugador
        Vector2 mouseDir = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        // Mirar en la dirección
        transform.up = mouseDir;
    }
}
