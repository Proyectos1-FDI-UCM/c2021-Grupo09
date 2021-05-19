using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaExplosiva : MonoBehaviour
{
    public int DañoPlayer;
    public int DañoEnemigo;
    private float momentoSpawn;
    public float momentoMuerte = 1;

    private void Start()
    {
        momentoSpawn = Time.time;
    }

    private void Update()
    {
        if (Time.time >= momentoSpawn + momentoMuerte)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            GameManager.GetInstance().HurtPlayer(DañoPlayer);
        }
        else if(other.gameObject.GetComponent<RecibaDanyo>() != null)
        {
            other.gameObject.GetComponent<RecibaDanyo>().DanarEnemigo(DañoEnemigo);
        }
    }
}
