using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        AudioManager.GetInstance().PlayMUSIC("MainMenuMusic");
        Cursor.SetCursor(null, new Vector2(0, 0), CursorMode.Auto);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void ClickEffect()
    {
        AudioManager.GetInstance().PlaySFX("Click");
    }
}
