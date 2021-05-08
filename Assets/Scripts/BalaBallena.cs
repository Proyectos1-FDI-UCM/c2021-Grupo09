using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaBallena : MonoBehaviour
{
    public float velocidadBala;
    private Rigidbody2D rb;
    public int damageDealt = 5;
    public GameObject zonaExplosiva, sombraBalaBallena;
    ShooterBallena sb;
    bool hasRotated;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sb = GetComponentInParent<ShooterBallena>();
        
        hasRotated = false;
    }
    

    void Update()
    {
        rb.velocity = transform.up * velocidadBala;
        if (!hasRotated && transform.position.y > 14)
        {
            Vector2 vector = new Vector2(sb.GetEnemyX(), 14f);
            rb.MovePosition(vector);
            rb.MoveRotation(180);
            hasRotated = true;
            Instantiate(sombraBalaBallena, new Vector2(sb.GetEnemyX(), sb.GetEnemyY()), Quaternion.identity);
        }
        if (hasRotated && transform.position.y < sb.GetEnemyY())
        {
            Instantiate(zonaExplosiva, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
            sb.ChangeIsShooting();
            Destroy(gameObject);
        }
    }
}
