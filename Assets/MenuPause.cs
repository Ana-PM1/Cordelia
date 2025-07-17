using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject opcionesMenuUI;

    void Update()
    {
        // Detecta tecla Escape para pausar/reanudar
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    //  pausar el juego 
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
    }

    //reanudar el juego 
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
    }

    // salir del juego
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }

    public void OpenOptions()
    {
        opcionesMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
}
