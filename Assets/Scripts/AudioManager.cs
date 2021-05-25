using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioMixerGroup SFXGroup;

    public AudioMixerGroup MusicGroup;

    public Sound[] sfx;

    public Sound[] music;

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


        foreach (Sound s in sfx)
        {
            s.fuente = gameObject.AddComponent<AudioSource>();
            s.fuente.clip = s.clip;
            s.fuente.outputAudioMixerGroup = SFXGroup;

            Debug.Log(s.fuente.outputAudioMixerGroup);

            s.fuente.volume = s.volumen;
        }

        foreach (Sound m in music)
        {
            m.fuente = gameObject.AddComponent<AudioSource>();
            m.fuente.clip = m.clip;
            m.fuente.outputAudioMixerGroup = MusicGroup;

            Debug.Log(m.fuente.outputAudioMixerGroup);

            m.fuente.volume = m.volumen;
        }
    }

    public static AudioManager GetInstance()
    {
        return instance;
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfx, sfx => sfx.nombre == name);
        s.fuente.Play();
    }

    public void PlayMUSIC(string name)
    {
        Sound m = Array.Find(music, music => music.nombre == name);
        m.fuente.Play();
    }

}
