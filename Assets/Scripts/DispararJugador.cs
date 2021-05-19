using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Añadir a un objeto hijo del jugador, que actúe de origen de las balas. El origen puede ser hijo // de un gameobject vacío con el script PointAtMouse, para darle un offset al cañón.

public class DispararJugador : MonoBehaviour
{
    public GameObject gameObjectBala;
    private float tiempoAux = 0;
    public float shooterCooldown;

    public void Shoot()
    {
        if (tiempoAux <= 0)
        {
            FindObjectOfType<AudioManager>().Play("Disparo"); // Reproducción del sonido del disparo
            // Crea bala donde esté el origen, mirando hacia donde mira este GameObject (hacia el ratón)
            Instantiate(gameObjectBala, transform.position, transform.rotation);

            tiempoAux = shooterCooldown;
        }
    }

    void Update()
    {
        //El tiempo auxiliar sera ese tiempo menos el actual
        tiempoAux = tiempoAux - Time.deltaTime;
    }
}
