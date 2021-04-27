using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text textoMonedas;
    void Start()
    {
        GameManager.GetInstance().SetUIManager(this);
    }
    public void UpdateMonedas(int totalMonedas)
    {
        textoMonedas.text = totalMonedas.ToString();
    }
}
