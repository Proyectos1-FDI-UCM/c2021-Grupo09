using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyKey : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Input.anyKey)
        {
            GameManager.GetInstance().PressAnyKey(true);
        }
    }
}
