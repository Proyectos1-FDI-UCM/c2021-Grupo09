using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Oleada // Constructor de Oleadas
    {
        public int maxEnemigos; // Número máximo de enemigos por oleada
        public GameObject enemigo; // GO a instanciar
    }

    private bool activateSpawn = true; // Booleano que controla activateSpawn

    private float cooldown; // Cooldown entre cada instancia de enemigos

    public Oleada[] oleadas; // Array de oleadas

    private void Start()
    {

        InvokeRepeating("Spawn", 2f, oleadas[GameManager.GetInstance().oleadaActual].maxEnemigos);

        // Si el spawn esta activado llama de manera continuada a la función Spawn
        /*for (int i=0; i < oleadas.Length; i++)
        {
            for (int j=0; j < oleadas[i].maxEnemigos; j++)
            {
                InvokeRepeating("Spawn", 0f, cooldown);
                Debug.Log("Estoy spawneando " + oleadas[i].maxEnemigos + " enemigos");
                GameManager.GetInstance().EnemigosNivel(oleadas[i].maxEnemigos);
            }
            CancelInvoke("Spawn");
        }*/
    }
    
    // Si ya se ha invocado un número de enemigos establecido se deja de invocar.
    private void Update()
    {
        while (activateSpawn)
        {
            //InvokeRepeating("Spawn", 2f, oleadas[GameManager.GetInstance().oleadaActual].maxEnemigos);
            Debug.Log("a");
            activateSpawn = GameManager.GetInstance().CambioRonda();
            Debug.Log("b");
        }

        //activateSpawn = GameManager.GetInstance().CambioRonda();
    }

    public void Spawn() // Spawn del enemigo
    {
        Instantiate(oleadas[GameManager.GetInstance().oleadaActual].enemigo, transform.position, transform.rotation);
    }
}
