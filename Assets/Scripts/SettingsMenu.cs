using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public static bool enPausa = false;
    public GameObject menuPausa;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (enPausa)
            {
                Pausar();
            }
            else
            {
                Continuar();
            }
        }
    }

    public void SetMUSICVolume(float volume)
    {
        audioMixer.SetFloat("music", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("sfx", volume);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        enPausa = true;
    }

    public void Continuar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        enPausa = false;
    }

}
