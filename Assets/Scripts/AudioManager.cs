using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public Sound[] sonidos;

    void Awake()
    {
        if (instance == null)
        {
            // Asegurar que solo exista una instancia del Game Manager
            instance = this;
            // Evita que el gameManager se destruya al cambiar de escena
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // Si hay mas de una instancia, se destruye
            Destroy(this.gameObject);
        }


        foreach (Sound s in sonidos)
        {
            s.fuente = gameObject.AddComponent<AudioSource>();
            s.fuente.clip = s.clip;

            s.fuente.volume = s.volumen;
        }
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sonidos, sonidos => sonidos.nombre == name);
        s.fuente.Play();
    }

}
