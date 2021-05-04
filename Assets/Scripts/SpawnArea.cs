using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    public GameObject enemigoSpawneadoPrefab;

    public void Spawn()
    {
        Instantiate(enemigoSpawneadoPrefab, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
    }
}
