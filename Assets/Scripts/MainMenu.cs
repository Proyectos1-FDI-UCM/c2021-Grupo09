using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
    }

    public void PlayGame()
    {
        AudioManager.GetInstance().PlaySFX("Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        AudioManager.GetInstance().PlaySFX("Click");
        Debug.Log("QUIT");
        Application.Quit();
    }
}
