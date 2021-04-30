using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private UIManager theUIManager;

    public int monedasTotal;
    public int valorMoneda;
    public int oleadaActual = 0;
    public int enemigosRest;


    void Awake()
    {

        if (instance == null)
        {
            //Asegurar que solo exista una instancia del Game Manager
            instance = this;
            //Evita que el gameManager se destruya al cambiar de escena
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //Si hay mas de una instancia, se destruye
            Destroy(this.gameObject);
        }
    }
    //Acceso a la instancia del Game Manager
    public static GameManager GetInstance()
    {
        return instance;
    }
    
    public void AddCoins()
    {
        monedasTotal += valorMoneda;
        //Actualiza el contador de monedas de la UI
        theUIManager.UpdateMonedas(monedasTotal);
    }
    public void SubtractCoins(int costeTorre)
    {
        monedasTotal -= costeTorre;
        theUIManager.UpdateMonedas(monedasTotal);
    }
    public int GetCoins()
    {
        return monedasTotal;
    }

    public void EnemigoCaido()
    {
        enemigosRest--;
    }

    public bool CambioRonda()
    {
        if (enemigosRest == 0)
        {
            oleadaActual++;
            return true;
        }

        else
        {
            return false;
        }
    }

    public void SetUIManager(UIManager uim)
    {
        theUIManager = uim;
    }
}
