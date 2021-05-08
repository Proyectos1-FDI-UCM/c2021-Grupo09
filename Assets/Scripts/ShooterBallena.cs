using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBallena : MonoBehaviour
{
    public GameObject balaBallena;
    public float shootCooldown;
    Transform enemyTransform;
    float enemyX, enemyY;
    float lastShotTime;
    bool isShooting;
    // Start is called before the first frame update
    void Start()
    {
        isShooting = false;
        lastShotTime = -shootCooldown;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isShooting && Time.time >= (lastShotTime + shootCooldown))
        {
            enemyTransform = collision.transform;
            enemyX = collision.transform.position.x;
            enemyY = collision.transform.position.y;
            Instantiate(balaBallena, transform.position, Quaternion.Euler(0, 0, 0), gameObject.transform);
            isShooting = true;
            lastShotTime = Time.time;
        }
    }
    public float GetEnemyX()
    {
        return enemyX;
    }
    public float GetEnemyY()
    {
        return enemyY;
    }
    public void ChangeIsShooting()
    {
        isShooting = false;
    }
}
