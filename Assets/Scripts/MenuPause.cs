using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    // Este script controla el menú de pausa del juego
    // Permite pausar el juego, reanudarlo, reiniciar el nivel y salir del juego
    // También permite abrir un menú de opciones
    // El menú de pausa se activa al presionar la tecla Escape
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

    // reiniciar el nivel actual
    // Borra datos anteriores y reinicia el nivel
    // Utiliza el GameFlowManager para reiniciar el nivel
    // Esto permite que el jugador comience de nuevo sin perder el progreso guardado
    public void ReiniciarNivel()
    {
        // Borra datos anteriores
        Time.timeScale = 1f;
        SaveManager.BorrarDatos();
        GameFlowManager.Instance.IniciarNivel(SceneManager.GetActiveScene().name);
    }

    // salir del juego
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is quitting");
    }
    // Abre el menú de opciones
    // Esto permite al jugador ajustar configuraciones del juego como volumen, resolucion, etc.
    public void OpenOptions()
    {
        opcionesMenuUI.SetActive(true);
        pauseMenuUI.SetActive(false);
    }
}
