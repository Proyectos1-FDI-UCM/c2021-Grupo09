using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    // Start is called before the first frame update

    public int monedasTotal = 0;
    public int valorMoneda;

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
