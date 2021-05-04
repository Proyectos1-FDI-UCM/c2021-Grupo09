using UnityEngine;

public class ShooterTortuga : MonoBehaviour
{
    public GameObject ray;
    GameObject myRay;
    int counter = 0;
    bool active = false;

    void Start()
    {
        myRay = Instantiate(ray);
        myRay.transform.position = new Vector3(transform.position.x + 0.14f, transform.position.y + 0.27f, -1);
        myRay.SetActive(false);
    }
    
    private void FixedUpdate()
    {
        counter++;
        if (counter > 100)
        {
            myRay.SetActive(true);
            active = true;
            counter = 0;
        }
        else if (active && counter > 50)
        {
            myRay.SetActive(false);
            active = false;
        }
    }
}
