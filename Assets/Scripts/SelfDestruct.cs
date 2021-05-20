using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float lifetime;
    float spawnTime;
    ShooterBallena sb;
    // Start is called before the first frame update
    void Start()
    {
        spawnTime = Time.time;
        if (GetComponentInChildren<ShooterBallena>()) sb = GetComponentInChildren<ShooterBallena>();
    }

    // Update is called once per frame
    void Update()
    {
        // Si la torre es ballena
        if (sb)
        {
            if (!sb.IsShooting() && Time.time > spawnTime + lifetime) Destroy(gameObject);
        }
        else if (Time.time > spawnTime + lifetime)
        {
            Destroy(gameObject);
        }
        
    }
    
}
