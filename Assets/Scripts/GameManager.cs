using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private UIManager theUIManager;

    public int monedasTotal;
    int monedasIniciales = 120;
    public int valorMoneda;
    public int oleadaActual = 0;

    int vidaJug;
    public int vidaMaxJug = 100;
    public GameObject jugador;

    int vidaBase;
    public int vidaMaxBase = 100;

    public bool playerWon;
    public int nivel = 0;
    public string[] scenesInOrder;

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

    private void Start()
    {
        vidaJug = vidaMaxJug;
        vidaBase = vidaMaxBase;
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
        theUIManager.UpdateUI(monedasTotal, vidaJug, vidaBase);
    }
    public void SubtractCoins(int costeTorre)
    {
        monedasTotal -= costeTorre;
        theUIManager.UpdateUI(monedasTotal, vidaJug, vidaBase);
    }
    public int GetCoins()
    {
        return monedasTotal;
    }

    public void SetUIManager(UIManager uim)
    {
        theUIManager = uim;
    }

    //Cuando el jugador muere
    public void DeadPlayer()
    {
        //Lost State. Debe mostrarse por interfaz y reiniciarse el nivel o ir al menu principal.

        playerWon = false;
        EndLevel(playerWon);
    }

    //Cuando se pierde la partida
    public void LostGame()
    {
        nivel = 2;
        ChangeScene(scenesInOrder[nivel]);
        if (Input.anyKey)
        {
            Invoke(nameof(Restart),1);
        }
    }

    //Cuando se gana la aprtida
    public void WonGame()
    {
        nivel = 3;
        ChangeScene(scenesInOrder[nivel]);
        if (Input.anyKey)
        {
            Invoke(nameof(Restart), 1);
        }
    }

    //Cada vez que terminas un nivel
    public void EndLevel(bool victory)
    {
        if (victory == false)
        {
            Invoke(nameof(LostGame), 1);
        }
        else if(victory == true && nivel >= 2)
        {
            Invoke(nameof(WonGame), 1);
        }

        else
        {
            Invoke(nameof(NextLevel), 1);
        }
    }

    // Manejar los cambios de escena
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //Cuando pasas de nivel
    public void NextLevel()
    {
        nivel += 1;
        monedasTotal = monedasIniciales;
        vidaBase = vidaMaxBase;
        vidaJug = vidaMaxJug;
        ChangeScene(scenesInOrder[nivel]);
    }

    //Volver a empezar el juego
    public void Restart()
    {
        nivel = 0;
        monedasTotal = monedasIniciales;
        vidaBase = vidaMaxBase;
        vidaJug = vidaMaxJug;
        ChangeScene(scenesInOrder[nivel]);
    }

    public void HurtPlayer(int danyo)
    {
        vidaJug -= danyo;
        if (vidaJug <= 0)
        {
            Destroy(jugador);
            DeadPlayer();
        }
        theUIManager.UpdateUI(monedasTotal, vidaJug, vidaBase);
        Debug.Log(vidaJug + " restante.");
    }

    public void HealPlayer(int danyo)
    {
        vidaJug += danyo - (danyo + vidaJug - 100);
        theUIManager.UpdateUI(monedasTotal, vidaJug, vidaBase);
    }

    public void HurtBase(int danyo)
    {
        vidaBase -= danyo;
        if (vidaBase <= 0)
        {
            DeadPlayer();
        }
        theUIManager.UpdateUI(monedasTotal, vidaJug, vidaBase);
        Debug.Log("Vida Base: " + vidaBase);
    }
}
