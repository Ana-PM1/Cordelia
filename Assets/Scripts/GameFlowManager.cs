using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance; //Global instance para acceder desde otros scripts
    

    // Este script controla el flujo del juego
    // Permite iniciar niveles, volver al menú principal y verificar si hay partidas guardadas
    private void Awake()
    {
        if (Instance != null) 
        { 
            Destroy(gameObject); 
            return; 
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Método para iniciar un nivel
    // Utiliza TransitionManager para cargar la escena con transición
    private void Start()
    {
        // Carga configuraciones
        ConfigManager.Instance.CargarConfiguracion();

        SceneManager.LoadScene("Main Menu", LoadSceneMode.Additive);
    

        // Cargar menú principal después de sistema
        //TransitionManager.Instance.CargarEscenaConTransicion("Menu");

    }
    // Método para iniciar un nivel específico
    // Utiliza TransitionManager para cargar la escena con transición
    public void IniciarNivel(string nombreNivel)
    {
        TransitionManager.Instance.CargarEscenaConTransicion(nombreNivel);
    }
    // Método para volver al menú principal
    // Utiliza TransitionManager para cargar la escena del menú principal
    public void VolverAlMenu()
    {
        TransitionManager.Instance.CargarEscenaConTransicion("Main Menu");
    }
    // Método para verificar si hay una partida guardada
    // Utiliza SaveManager para comprobar si existen datos guardados
    public bool HayPartidaGuardada()
    {
        return SaveManager.ExistePartidaGuardada();
    }

    // ✅ Nuevo: Inicia el flujo con Intro antes del primer nivel
    public void IniciarNuevoJuego()
    {
        SaveManager.BorrarDatos();
        StartCoroutine(FlujoNuevoJuego());
    }

    private IEnumerator FlujoNuevoJuego()
    {
        // 1. Cargar escena Intro aditivamente
        AsyncOperation loadIntro = SceneManager.LoadSceneAsync("Intro", LoadSceneMode.Additive);
        yield return loadIntro;

        // 2. Buscar el VideoPlayer en la escena Intro
        VideoPlayer video = null;
        Scene introScene = SceneManager.GetSceneByName("Intro");
        foreach (GameObject root in introScene.GetRootGameObjects())
        {
            video = root.GetComponentInChildren<VideoPlayer>();
            if (video != null) break;
        }

        if (video != null)
        {
            bool videoTerminado = false;
            video.loopPointReached += (vp) => { videoTerminado = true; };

            // 3. Esperar a que termine el video o que el jugador presione tecla
            while (!videoTerminado && !Input.anyKeyDown)
            {
                yield return null;
            }
        }

        // 4. Descargar Intro
        AsyncOperation unloadIntro = SceneManager.UnloadSceneAsync("Intro");
        yield return unloadIntro;

        // 5. Cargar Nivel1 aditivamente
        TransitionManager.Instance.CargarEscenaConTransicion("Nivel1");
    }
}
