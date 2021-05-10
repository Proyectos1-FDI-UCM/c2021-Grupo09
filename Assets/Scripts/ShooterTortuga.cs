using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterTortuga : MonoBehaviour
{
    public float shootCooldown;
    float enemyX, enemyY;
    float lastShotTime;

    public GameObject ray;
    GameObject myRay;
    int counter = 0;
    bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        lastShotTime = -shootCooldown;

        myRay = Instantiate(ray);
        myRay.transform.position = new Vector3(transform.position.x + 0.14f, transform.position.y + 0.27f, -1);
        myRay.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Time.time >= (lastShotTime + shootCooldown))
        {
            enemyX = collision.transform.position.x;
            enemyY = collision.transform.position.y;
            myRay = Instantiate(ray, transform.position, Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(enemyX, enemyY) - transform.position), Time.deltaTime));
            lastShotTime = Time.time;
        }
        else if (Time.time >= lastShotTime + shootCooldown / 2)
        {
            Destroy(myRay);
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
    private void OnDestroy()
    {
        Destroy(myRay);
    }
}