using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterPulpo : MonoBehaviour
{
    public GameObject bulletPrefab; // Establecemos el prefab de la bala a la que le vamos a agregar el componente Shooter para habilitar el disparo
    public bool autoShoot; // Establecemos el resto de variables públicas para determinar si los disparos van a ser automáticos o manuales
    public float cadenciaDisp; // El retardo de tiempo entre cada disparo automático
    float shootTime;

    void Start()
    {
        if (autoShoot)// Si tenemos habilitada la opción de disparar (método Shoot) automáticamente crearemos copias continuamente de la bala cada cadenciaDisp segundos
        {
            Debug.Log("Disparo");
            InvokeRepeating("Shoot", cadenciaDisp, cadenciaDisp);
        }
    }

    //void Update()
    //{
    //    if (!autoShoot && Time.time > shootTime) // Si no tenemos habilitado el disparo automático y pulsamos la tecla espacio realizaremos un disparo manual
    //    {
    //        Shoot(); // Llamamos al método público Shoot para realizar el disparo
    //        shootTime = Time.time; // Establecemos el tiempo de recarga que va a tener cada disparo
    //    }
    //}

    public void Shoot() // Método público utilizado para el disparo
    {
        for (int i = 0; i < 8; i++)
        {
            // Instanciamos un clon del prefab de la bala 
            // en la escena en la posición del cañón del tanque (poseedor del componente Shooter)
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, 45 * i));
        }
    }
    void OnDestroy() // Activamos el despawn de las balas con la destrucción de la torre. Si la torre se destruye no se dispararán más balas
    {
        CancelInvoke(); // Cancelamos la invocación de las balas
    }
}
