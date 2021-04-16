using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //GO a instanciar
    public GameObject enemy;
    //booleando que controla la funcion ActivateSpawn
   	private bool activateSpawn = true;
   	//La diferencia que hay entre que spawnea un enemigo y el siguiente
    public float diferencia;
	//Int con el numero maximo de enemigos de la primera oleada
	public float maxEnemies;
    //Float para ayudar a parar el spawn
    private float stopSpawn;

    private void Start()
    {
        //Si el spawn esta activado llama de manera continuada a la función Spawn
        if (activateSpawn)
        {
            InvokeRepeating("Spawn", 0f, diferencia);

        }
    }
    
    //si ya se ha invocado un numero de enemigos establecido se deja de invocar.
    private void Update()
    { 
        stopSpawn = maxEnemies*diferencia;

	    if (Time.time >= stopSpawn)
	    {
            CancelInvoke("Spawn");
	 	   
	    }	
    }


    //método que Hace que spawnee el enemigo en la posicion y con la rotacion del GO al que este asociado.
    public void Spawn()
    {
  	    Instantiate(enemy, transform.position, transform.rotation);      

    }

}
