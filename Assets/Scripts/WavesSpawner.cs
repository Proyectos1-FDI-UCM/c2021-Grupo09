using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSpawner : MonoBehaviour
{

    public enum SpawnState { active, waiting, counting }; // Diferentes estados del spawn

    [System.Serializable]
    public struct Oleada // Constructor de Oleadas
    {
        public int maxEnemigos; // Número máximo de enemigos por oleada
        public GameObject enemigo; // GO a instanciar
    }

    public float cooldown = 5f; // Cooldown entre cada instancia de enemigos
    private float countdown; // Tiempo de espera hasta el inicio de una ronda
    private float searchCountdown = 1f;

    public Oleada[] oleadas; // Array de oleadas

    private SpawnState estado = SpawnState.counting; // Se inicia el estado del spawn a contando

    void Start()
    {
        countdown = cooldown; // Se inicializa la cuenta atrás como el 'cooldown'
    }


    void Update()
    {
        if (estado == SpawnState.waiting) // Si el estado es esperando
        {
            if (!EnemiesAlive()) // Y ya no quedan enemigos vivos
            {
                WaveCompleted(); // La oleada ha sido terminada
                return;
            }
            else
            {
                return;
            }
        }

        if (countdown <= 0) // Si el contador está en 0
        {
            if (estado != SpawnState.active) // Y el spawn no está activo
            {
                // Seguirán apareciendo oleadas de enemigos
                StartCoroutine(SpawnWave(oleadas[GameManager.GetInstance().oleadaActual]));
            }
        }
        else // De lo contrario
        {
            countdown -= Time.deltaTime; // Se inicializará el contador
        }

        bool EnemiesAlive() // Booleano para comprobar si quedan o no enemigos
        {
            searchCountdown -= Time.deltaTime;
            if (searchCountdown <= 0f)
            {
                searchCountdown = 1f;
                if (gameObject.layer == 12)
                {
                    return false;
                }
            }
            return true;
        }

        void WaveCompleted() // Método para comprobar si se ha completado la oleada actual
        {
            estado = SpawnState.counting; // Se inicializa el estado en contando
            countdown = cooldown;

            if (GameManager.GetInstance().oleadaActual + 1 > oleadas.Length - 1)
            {
                GameManager.GetInstance().oleadaActual = 0;
                Debug.Log("Se acabó, has ganado :)");
            }
            else
            {
                GameManager.GetInstance().oleadaActual++;
            }
        }

        IEnumerator SpawnWave(Oleada _oleada) // Se crea una corrutina
        {
            estado = SpawnState.active; // Se inicializa el estado a activo

            // Se inicializan tantos enemigos como se declaren en el editor
            for (int i = 0; i < oleadas[GameManager.GetInstance().oleadaActual].maxEnemigos; i++)
            {
                Instantiate(oleadas[GameManager.GetInstance().oleadaActual].enemigo, transform.position, transform.rotation);
                yield return new WaitForSeconds(2f); // Tiempo de espera de 2s entre instancias
            }

            estado = SpawnState.waiting; // Cuando acabe el estado se mantendrá en espera

            yield break;
        }
    }
}
