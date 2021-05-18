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
    public RectTransform[] iconosTorres = new RectTransform[4];

    void Start()
    {
        GameManager.GetInstance().SetUIManager(this);
    }
    public void UpdateUI(int totalMonedas, int vidaJug, int vidaBase, int torreGrande)
    {
        textoMonedas.text = totalMonedas.ToString();
        barraPlayer.value = vidaJug;
        barraBase.value = vidaBase;

        if (torreGrande > -1)
        {
            for (int i = 0; i < iconosTorres.Length; i++)
            {
                if (i == torreGrande)
                {
                    iconosTorres[i].localScale = new Vector3(1.3f, 1.3f);
                }
                else
                {
                    iconosTorres[i].localScale = new Vector3(0.7f, 0.7f);
                }
            }
        }
        else
        {
            for (int i = 0; i < iconosTorres.Length; i++)
            {
                iconosTorres[i].localScale = new Vector3(1, 1);
            }
        }
        
    }
}
