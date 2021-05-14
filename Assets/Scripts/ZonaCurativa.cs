using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaCurativa : MonoBehaviour
{
    public int CuraPlayer;
    public int CuraEnemigo;
    public float delayCura;
    private float momentoCura;
    private float momentoSpawn;
    public float momentoMuerte = 6;

    private void Start()
    {
        momentoSpawn = Time.time;
    }

    private void Update()
    {
        if(Time.time >= momentoSpawn + momentoMuerte)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time >= (momentoCura + delayCura))
        {
            momentoCura = Time.time;
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                Debug.Log(Time.time);
                GameManager.GetInstance().HealPlayer(CuraPlayer);
            }
            else if(other.gameObject.GetComponent<RecibaDanyo>() != null) // Necesario por si se ha destruido el objeto mientras
            {
                other.gameObject.GetComponent<RecibaDanyo>().CurarEnemigo(CuraEnemigo);
            }
        }
    }
}
