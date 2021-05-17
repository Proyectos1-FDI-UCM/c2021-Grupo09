using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sonidos;

    void Awake()
    {
        foreach (Sound s in sonidos)
        {
            s.fuente = gameObject.AddComponent<AudioSource>();
            s.fuente.clip = s.clip;

            s.fuente.volume = s.volumen;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sonidos, sonidos => sonidos.nombre == name);
        s.fuente.Play();
    }

}
