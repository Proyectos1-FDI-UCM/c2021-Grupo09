using System.Collections.Generic;
using UnityEngine;

public class RayoTortuga : MonoBehaviour
{
    public int DañoPlayer;
    public int DañoEnemigo;
    public float cadenciaDaño;
    float ultimoDaño;
    List<GameObject> targets = new List<GameObject>();
    ContactFilter2D filter;

    private void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>().color.a == 1)
        {
            if(Time.time > ultimoDaño + cadenciaDaño)
            {
                foreach (GameObject g in targets)
                {
                    if (g.GetComponent<PlayerController>()) GameManager.GetInstance().HurtPlayer(DañoPlayer);
                    else if(g.GetComponent<RecibaDanyo>()) g.GetComponent<RecibaDanyo>().DanarEnemigo(DañoEnemigo);
                }
                ultimoDaño = Time.time;
            }
        }
        else if (gameObject.GetComponent<SpriteRenderer>().color.a == 0 && !targets.Count.Equals(0)) targets.Clear();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        targets.Add(other.gameObject);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(targets.Contains(other.gameObject)) targets.Remove(other.gameObject);
    }
}
