﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartelControles : MonoBehaviour
{
    public void ChangeSetActive()
    {
        if (gameObject.active)
            gameObject.SetActive(false);
        else
            gameObject.SetActive(true);
    }
}
