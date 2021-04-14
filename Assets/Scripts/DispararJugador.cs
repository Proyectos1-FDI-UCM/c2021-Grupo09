using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Añadir a un objeto hijo del jugador, que actúe de origen de las balas. El origen puede ser hijo // de un gameobject vacío con el script PointAtMouse, para darle un offset al cañón.

public class DispararJugador : MonoBehaviour
{
    public GameObject gameObjectBala;

    public void Shoot()
    {
        // Crea bala donde esté el origen, mirando hacia donde mira este GameObject (hacia el ratón)
        Instantiate(gameObjectBala, transform.position, transform.rotation);
    }
}
