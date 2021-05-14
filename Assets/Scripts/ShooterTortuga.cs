using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTortuga : MonoBehaviour
{
    enum estadoTortuga { espera, apunta, dispara}
    public float shootCooldown;
    float lastShotTime;
    // shootCooldown es el tiempo entre que empieza un disparo y el siguiente. 1/4 del tiempo estará apuntando, otro 1/4 disparando
    // y el otro 1/2 estará recargando, en estado de espera

    public GameObject ray;
    GameObject myRay;
    Transform enemy;
    bool followEnemy = false;
    estadoTortuga estado;

    void Start()
    {
        lastShotTime = -shootCooldown;

        myRay = Instantiate(ray);
        myRay.transform.position = new Vector3(transform.position.x + 0.3f, transform.position.y + 0.3f, -1);
        myRay.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 0);
        estado = estadoTortuga.espera;
    }
    private void Update()
    {
        // Si hay enemigo, apunta en su dirección
        if (followEnemy && enemy != null)
        {
            myRay.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(enemy.position.y - transform.position.y, enemy.position.x - transform.position.x) * Mathf.Rad2Deg);
        }

        if (estado == estadoTortuga.dispara && Time.time >= lastShotTime + shootCooldown / 2)
        {
            // Se acaba el disparo
            myRay.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 0);
            estado = estadoTortuga.espera;
        }
        else if (estado == estadoTortuga.apunta && Time.time >= lastShotTime + shootCooldown / 4)
        {
            // Deja de apuntar y comienza el disparo en sí
            myRay.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 1); // Color opaco
            myRay.transform.localScale = new Vector3(myRay.transform.localScale.x, 2 * myRay.transform.localScale.y, 1); // Mayor grosor
            followEnemy = false; // Tiene que dejar de perseguir
            estado = estadoTortuga.dispara;
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if (Time.time >= (lastShotTime + shootCooldown))
        {
            enemy = collider.transform; // Se guarda el objetivo
            myRay.GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 0.2f); // Semi-transparente para el apuntado
            myRay.transform.localScale = new Vector3(myRay.transform.localScale.x, 0.5f*myRay.transform.localScale.y, 1); // Más estrecho en el apuntado
            followEnemy = true; // Tiene que perseguir al objetivo
            estado = estadoTortuga.apunta;
            lastShotTime = Time.time;
        }
    }
}