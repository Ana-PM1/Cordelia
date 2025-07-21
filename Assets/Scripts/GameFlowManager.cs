using UnityEngine;
using UnityEngine.SceneManagement;

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
}
