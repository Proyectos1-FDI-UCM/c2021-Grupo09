using UnityEngine;

public class ShooterTortuga : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics2D.Raycast(transform.position, new Vector3(1, 0, 0), Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, new Vector3(1, 0, 0), Color.green, 10, true);
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector3(1, 0, 0), Color.red, 10, true);
        }
    }
}
