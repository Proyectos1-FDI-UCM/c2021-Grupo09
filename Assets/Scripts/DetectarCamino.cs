using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarCamino : MonoBehaviour
{
    public Material transparent;
    // Se cambiara en el GameManager
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Renderer>().material == transparent)
        {
            player.GetComponent<PlayerController>().CambiaPuedeConstruir(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Renderer>().material == transparent)
        {
            player.GetComponent<PlayerController>().CambiaPuedeConstruir(false);
        }
    }
}
