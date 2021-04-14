using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AñadirAPlayerController : MonoBehaviour
{

    enum ModoJug {Disparo, Construccion}

    struct PlayerInfo
    {
        public ModoJug modo;
    }

    PlayerInfo playerInfo;

    // Start is called before the first frame update
    void Start()
    {
        playerInfo.modo = ModoJug.Disparo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (playerInfo.modo == ModoJug.Disparo)
                GetComponentInChildren<DispararJugador>().Shoot();
        }

        if (Input.GetButtonDown("Fire2"))
        {
            if (playerInfo.modo == ModoJug.Disparo)
                playerInfo.modo++;

            else
                playerInfo.modo--;

            Debug.Log(playerInfo.modo);
        }

    }
}
