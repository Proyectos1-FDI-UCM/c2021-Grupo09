using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string nombre;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volumen;

    [HideInInspector]
    public AudioSource fuente;
}
