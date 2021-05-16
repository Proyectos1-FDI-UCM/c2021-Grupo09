using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_Text textoMonedas;
    public Slider barraPlayer;
    public Slider barraBase;

    void Start()
    {
        GameManager.GetInstance().SetUIManager(this);
    }
    public void UpdateUI(int totalMonedas, int vidaJug, int vidaBase)
    {
        textoMonedas.text = totalMonedas.ToString();
        barraPlayer.value = vidaJug;
        barraBase.value = vidaBase;
    }
}
